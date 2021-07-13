using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Tree
    {
        private Node root; 
        private string json;
        private LinkedList<int> eq;
        private string prolog = "";
        public Tree(ref Node n)
        {
            root = n;
            json = "";
            eq = new LinkedList<int>();
            
        }
        public Tree(ref Node n, string inputText)
        {
            int index = 0;
            if (inputText.Length >= 2) {
                if(inputText.Substring(1, 4) == "?xml")
                {
                    List<char> temp = new List<char>();
                    string tempStr;
                    while(inputText[index] != '>')
                    {
                        temp.Add(inputText[index]);
                        if (index < inputText.Length - 1) index++; else break;
                    }
                    tempStr = new string(temp.ToArray());
                    prolog = tempStr;
                    if (index < inputText.Length - 1) index++;
                    if (index < inputText.Length - 1) index++;
                }
            }
            root = parseNode(inputText, ref index);
            n = root;
        }
        private Node parseNode(string inputXML, ref int currentIndex)
        {
            Node currentNode = new Node();
            List<char> temp = new List<char>();
            string tempStr;
            while (currentIndex < inputXML.Length)
            {
                if (inputXML[currentIndex] == '<')
                {
                    if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                    if (inputXML[currentIndex] != '/')
                    {
                        //Detect name
                        //TODO ignore comments (comment can be a tag too?)
                        while (inputXML[currentIndex] != ' ' && inputXML[currentIndex] != '>')
                        {
                            //Read tag name
                            temp.Add(inputXML[currentIndex]);
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        }
                        tempStr = new string(temp.ToArray());
                        currentNode.setName(tempStr);
                        temp.Clear();

                        //continue parsing the tag for attributes, values, and other tags (children)
                        while (inputXML[currentIndex] != '/' && inputXML[currentIndex] != '>')
                        {
                            //Read tag attribute name
                            while (inputXML[currentIndex] != '=')
                            {
                                if (inputXML[currentIndex] == ' ')
                                {
                                    if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                                    continue;
                                }
                                temp.Add(inputXML[currentIndex]);
                                if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            }
                            tempStr = new string(temp.ToArray());
                            currentNode.setOneAttr(tempStr);
                            temp.Clear();
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            while (inputXML[currentIndex] != '"')
                            {
                                //Read tag attribute value
                                temp.Add(inputXML[currentIndex]);
                                if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            }
                            tempStr = new string(temp.ToArray());
                            currentNode.setOneAttr(tempStr);
                            temp.Clear();
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            //Skip whitespace
                            while (inputXML[currentIndex] == ' ' || inputXML[currentIndex] == '\n' || inputXML[currentIndex] == '\t')
                                if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        }

                        //The case of self-closing tags
                        if(inputXML[currentIndex] == '/')
                        {
                            currentNode.setOneLine(true);
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            while (inputXML[currentIndex] == ' ' || inputXML[currentIndex] == '\n' || inputXML[currentIndex] == '\t')
                                if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            return currentNode;
                        }

                        //Skip whitespace
                        if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        while(inputXML[currentIndex] == ' ' || inputXML[currentIndex] == '\n' || inputXML[currentIndex] == '\t')
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        //Value or child tag?
                        if(inputXML[currentIndex] != '<')
                        {
                            //In case of value
                            while (inputXML[currentIndex] != '<' && inputXML[currentIndex] != '\n')
                            {
                                temp.Add(inputXML[currentIndex]);
                                if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                            }
                            tempStr = new string(temp.ToArray());
                            currentNode.setValue(tempStr);
                            temp.Clear();
                        }
                        else
                        {
                            //In case of tag
                            if (currentIndex + 1 < inputXML.Length) 
                                while (inputXML[currentIndex + 1] != '/')
                                {
                                    currentNode.setchild(parseNode(inputXML,ref currentIndex));
                                    if (!(currentIndex + 1 < inputXML.Length)) break;
                                }
                                //else
                                //{
                                //return currentNode;
                                //}
                        }
                        while(inputXML[currentIndex] != '>')
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        while (inputXML[currentIndex] == ' ' || inputXML[currentIndex] == '\n' || inputXML[currentIndex] == '\t')
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                    }
                }
                return currentNode;
            }
            return currentNode;
        }
        public string getProlog()
        {
            return prolog;
        }
        public void insert() { }
        public void format() { }
        public void conv2Json(ref Node r, ref LinkedList<int> e, int ind)
        {
            int i = 0;
            bool enter = false;
            if (r.getAllCh().Count == 0)
            {
                json = json + "}\n";
                return;
            }
            json = json + $"{r.getName()}: ";
            if (e.Count != 0 && ind == e.First.Value)
            {
                json = json + "[" + "\n";
            }
            json = json + "{";
            int cAttr = r.getAllAttr().Count;
            while (cAttr != 0)
            {
                enter = true;
                if (i % 2 == 0)
                    json = json + "\n" + $"@{r.getOneAttr()}: ";
                else
                    json = json + $"{r.getOneAttr()}";
                i++;
                cAttr--;
            }
            if (enter)
                json = json + "\n" + "#text: " + $"\"{r.getValue() }\"";
            else
                json = json + "\n" + $"\"{r.getValue() }\"";
            if (e.Count != 0 && ind != e.First.Value && ind != e.Last.Value && e.Contains(ind))
            {
                json = json + ",";
            }
            if (e.Count != 0 && ind != e.First.Value && ind == e.Last.Value)
            {
                json = json + "]";
            }
            for (int j = 0; j < r.getAllCh().Count; j++)
                for (int v = 0; v < r.getAllCh().Count; v++)
                {
                    if (j != v && r.getAllCh()[j].getName().Equals(r.getAllCh()[v].getName()))
                        e.AddLast(j);
                }
            for (int j = 0; j < r.getAllCh().Count; j++)
            {
                Node x = r.getAllCh()[j];
                conv2Json(ref x, ref e, j);
            }
            json = json + "}\n";
        } 
public string getJSON()
        {
            return json;
        }
    }
 }
