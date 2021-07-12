using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Tree
    {
        private Node root;
        public Tree(string inputXML)
        {
            //List<char> temp = new List<char>();
            //bool inTag = false;
            //bool inValue = false;
            //for (int i = 0; i < inputXML.Length; i++)
            //{
            //    if (inputXML[i] == '<')
            //    {
            //        i++;
            //        if (inputXML[i] != '/')
            //        {
            //            i++;
            //            while (inputXML[i] != ' ' && inputXML[i] != '>')
            //            {
            //                temp.Add(inputXML[i]);
            //                i++;
            //            }
            //            add to a node
            //        }
            //    }
            //}
        }
        private Node parseNode(ref string inputXML, int currentIndex)
        {
            Node currentNode = new Node();
            List<char> temp = new List<char>();
            while (currentIndex < inputXML.Length)
            {
                if (inputXML[currentIndex] == '<')
                {
                    currentIndex++;
                    if (inputXML[currentIndex] != '/')
                    {
                        currentIndex++;
                        while (inputXML[currentIndex] != ' ' && inputXML[currentIndex] != '>')
                        {
                            temp.Add(inputXML[currentIndex]);
                            currentIndex++;
                        }
                        currentNode.setName(temp.ToString());
                        //continue parsing the tag for attributes, values, and other tags (children)
                    }
                }
            }
            return currentNode;
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
