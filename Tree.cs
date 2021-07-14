using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Tree
    {
        int depthn = 0;
        private Node root; 
        private string json;
        private LinkedList<int> eq;
        private List<string> startingTags = new List<string>();
        private bool printedStart = true;
        public List<string> toBePrinted = new List<string>();

        public Tree(ref Node n)
        {
            root = n;
            json = "";
            eq = new LinkedList<int>();
            
        }
        public Tree(ref Node n, string inputText)
        {
            List<char> temp = new List<char>();
            string tempStr;
            int index = 0;
            while (true)
            {
                if (inputText.Length >= index + 4)
                {
                    if (inputText.Substring(index, 4) == "<!--" || inputText.Substring(index, 4) == "<?xm")
                    {
                        printedStart = false;
                        while (inputText[index] != '>')
                        {
                            temp.Add(inputText[index]);
                            if (index < inputText.Length - 1) index++; else break;
                        }
                        temp.Add(inputText[index]);
                        tempStr = new string(temp.ToArray());
                        startingTags.Add(tempStr);
                        temp.Clear();
                        if (index < inputText.Length - 1) index++;
                        while (inputText[index] == ' ' || inputText[index] == '\n' || inputText[index] == '\t')
                            if (index < inputText.Length - 1) index++; else break;
                    }
                    else break;
                }
                else break;
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
                        while (inputXML[currentIndex] != ' ' && inputXML[currentIndex] != '>')
                        {
                            //Read tag name
                            temp.Add(inputXML[currentIndex]);
                            if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                        }
                        tempStr = new string(temp.ToArray());
                        currentNode.setName(tempStr);
                        temp.Clear();

                        //Ignore comments (comment can be a tag too?)
                        if(tempStr.Length >= 3)
                            if(tempStr.Substring(0, 3) == "!--")
                            {
                                while (inputXML[currentIndex] != '>')
                                {
                                    temp.Add(inputXML[currentIndex]);
                                    if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                                }
                                temp.Add(inputXML[currentIndex]);
                                tempStr = new string(temp.ToArray());
                                currentNode.setOneAttr(tempStr);
                                temp.Clear();

                                if (currentIndex < inputXML.Length - 1) currentIndex++; else break;
                                while (inputXML[currentIndex] == ' ' || inputXML[currentIndex] == '\n' || inputXML[currentIndex] == '\t')
                                    if (currentIndex < inputXML.Length - 1) currentIndex++; else break;

                                currentNode.setCommentFlag(true);
                                //currentNode.setOneLine(true);
                                return currentNode;
                            }

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
        public List<string> getStartingTags()
        {
            return startingTags;
        }
        public void insert() { }
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


        public int Depth(ref Node n, ref Node s)
        {
            if (n.getName() == s.getName())
            {
                return 0;
            }
            else
            {

                //if (s.getCountCh() != 0)
                //{
                ++depthn;
                //}
                List<Node> data = s.getAllCh();

                for (int i = 0; i < s.getCountCh(); i++)
                {
                    if (data[i].getName() == n.getName()) return depthn;
                }

                for (int j = 0; j < s.getCountCh(); j++)
                {
                    Node d = data[j];
                    //if (s.getCountCh() != 0)
                    return Depth(ref n, ref d);  //error data[j] 

                }
                return 1;
            }


        }
        public void Space(Node k)
        {
            int n = 0;
            depthn = 0;
            n = Depth(ref k, ref root);
            // Console.Write(n);
            for (int i = 0; i < n; i++)
            {
                //Console.Write(" ");
                toBePrinted.Add("    ");
            }
        }
        public void print(Node r, ref int dep)
        {
            bool commentFlag = false;
            if (printedStart == false)//// print startinftags
            {
                List<string> Tags = getStartingTags();
                for (int i = 0; i < Tags.Count; i++)
                {
                    //Console.WriteLine(Tags[i]);
                    toBePrinted.Add(Tags[i] + "\n");
                }
                printedStart = true;
            }
            //Space(r);
            for (int i = 0; i < dep; i++)
            {
                //Console.Write(" ");
                toBePrinted.Add("    ");
            }
            if (r.getName().Length >= 3)//to print comment
            {
                if(r.getName().Substring(0, 3) == "!--")
                {
                    commentFlag = true;
                    //Console.Write("<" + r.getName() + " " + r.getAllAttr().Dequeue());
                    toBePrinted.Add("<" + r.getName() + " " + r.getAllAttr().Dequeue() + "\n");
                }
            }
            if(commentFlag == false)
            {
                if (r.getOneLine() == true)/// to print <example />
                {
                    //Console.Write("<" + r.getName());
                    toBePrinted.Add("<" + r.getName());
                    if (r.getCountAttr() != 0)
                    {
                        Queue<string> attributes = r.getAllAttr();
                        int count = attributes.Count;
                        for(int i = 0; i < count; i+=2)
                        {
                            //Console.Write(" " + s);
                            toBePrinted.Add(" ");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("=\"");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("\"");
                        }
                        //toBePrinted.Add("/>" + "\n");
                    }
                    //Console.Write(" />");
                    toBePrinted.Add(" />" + "\n");

                }
                else
                {
                    //Console.Write("<" + r.getName());
                    toBePrinted.Add("<" + r.getName());
                    if (r.getCountAttr() != 0)
                    {
                        //foreach (string s in r.getAllAttr())
                        //{
                        //    //Console.Write(" " + s + ">");
                        //    toBePrinted.Add(" " + s + ">");
                        //}
                        Queue<string> attributes = r.getAllAttr();
                        int count = attributes.Count;
                        for (int i = 0; i < count; i+=2)
                        {
                            //Console.Write(" " + s);
                            toBePrinted.Add(" ");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("=\"");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("\"");
                        }
                        //toBePrinted.Add(">");
                    }
                    toBePrinted.Add(">");
                    //Console.Write(r.getValue());
                    toBePrinted.Add(r.getValue());
                    List<Node> child = r.getAllCh();
                    if (r.getCountCh() != 0)
                    {
                        //Console.WriteLine("\n");
                        toBePrinted.Add("\n");
                        for (int i = 0; i < r.getCountCh(); i++)
                        {
                            dep += 1;
                            print(child[i], ref dep);
                            dep -= 1;
                        }
                    }
                    if (r.getCountCh() != 0)
                        //Space(r);
                        for (int i = 0; i < dep; i++)
                        {
                            //Console.Write(" ");
                            toBePrinted.Add("    ");
                        }
                    //Console.Write("</" + r.getName() + ">");
                    //Console.WriteLine("\n");
                    toBePrinted.Add("</" + r.getName() + ">" + "\n");
                }

            }
        }

        public void format()
        {
            int dep = 0;
            print(root,ref dep);
        }

        public void Mini(Node k)
        {
            if (!printedStart)
            {
                foreach (string tag in startingTags)
                {
                    if(tag.Length >= 4)
                    {
                        if (tag.Substring(0, 4) == "<!--") continue;
                    }
                    toBePrinted.Add(tag);
                }
                printedStart = true;
            }
            if (k.getCommentFlag() == false)//remove all comments
            {
                if (k.getOneLine() == true)
                {
                    //Console.Write("<" + k.getName());
                    toBePrinted.Add("<" + k.getName());
                    if (k.getCountAttr() != 0)
                    {
                        //foreach (string s in k.getAllAttr())
                        //{
                        //    //Console.Write(" " + s);
                        //    toBePrinted.Add(" " + s);
                        //}
                        Queue<string> attributes = k.getAllAttr();
                        int count = attributes.Count;
                        for (int i = 0; i < count; i += 2)
                        {
                            //Console.Write(" " + s);
                            toBePrinted.Add(" ");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("=\"");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("\"");
                        }
                    }
                    //Console.Write(" />");
                    toBePrinted.Add(" />");
                }
                else
                {
                    //Console.Write("<" + k.getName());
                    toBePrinted.Add("<" + k.getName());

                    if (k.getCountAttr() != 0)
                    {
                        //Console.Write(" ");
                        toBePrinted.Add(" ");
                        //foreach (string s in k.getAllAttr())
                        //{
                        //    //Console.Write(s + " ");
                        //    toBePrinted.Add(s + " ");
                        //}
                        Queue<string> attributes = k.getAllAttr();
                        int count = attributes.Count;
                        for (int i = 0; i < count; i += 2)
                        {
                            //Console.Write(" " + s);
                            toBePrinted.Add(" ");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("=\"");
                            toBePrinted.Add(attributes.Dequeue());
                            toBePrinted.Add("\"");
                        }
                    }
                    toBePrinted.Add(">");
                    //Console.Write(k.getValue());
                    toBePrinted.Add(k.getValue());
                    List<Node> child = k.getAllCh();
                    if (k.getCountCh() != 0)
                    {

                        for (int i = 0; i < k.getCountCh(); i++)
                        {
                            Mini(child[i]);
                        }

                    }
                    //Console.Write("</" + k.getName() + ">");
                    toBePrinted.Add("</" + k.getName() + ">");
                }
            }
        }

        public void Minifying()
        {
            toBePrinted.Clear();
            printedStart = false;
            Mini(root);
        }

    }
}
