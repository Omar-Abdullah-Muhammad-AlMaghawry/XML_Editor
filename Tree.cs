using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace XML_editor
{
    class Tree
    {
        private Node root;
        private string json0 ;
        private List<int> tempRe;
        private Queue<int> temQu;
        private int tempIndex;

        int depTemp;
        private string prolog = "";

        public Tree(ref Node n)
        {
            root = n;
            json0 = "";
            tempRe = new List<int>();
            depTemp = 0;
           tempIndex = -1;


        }
        public Tree(ref Node n, string inputText)
        {
            tempRe = new List<int>();
            depTemp = 0;
            json0 = "";
            tempIndex=-1;

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
        public int getDepth(ref Node r)
        {
            if (r.getCountCh() == 0)
                return 0;
            List<int> d= new List<int>();
            for (int i =0;i<r.getCountCh();i++)
            {
                Node x = r.getAllCh()[i];
                d.Add(getDepth(ref x));
            }
            d.Sort();
            return d[d.Count - 1]+1;
        }
        public int getDepthNode(ref Node r, ref Node n)
        {
            if (r==null)
                return 0;
            
            int c = -1;
            int i;
            Node x;
            for ( i = 0; i < r.getCountCh(); i++)
            {
               
         
                x= r.getAllCh()[i];

                if (r == n || (c = getDepthNode(ref x, ref n)) >= 0)
                    return c+1;
            }

          
            return c;
        }
       // public void conv2Json(ref Node r, ref Queue<int> e, ref List<int> repeat, int ind, bool equal, int depth, ref string json, bool hasCh)
        public void conv2Json(ref Node r, int ind, bool equal, int depth, ref string json)

        {

            Queue<int> e = new Queue<int>();
            List<int> repeat1= new List<int>();
            int i = 0;
            bool enter = false;
            //    
            Node m = r;
            for (int v = 0; v < r.getCountCh(); v++)
            {
                int j = r.getAllCh().FindIndex(v + 1,
                     delegate (Node n)
                     {
                         return n.getName() == m.getAllCh()[v].getName();
                     });
                if (j > 0)
                {
                    r.getAllCh()[j].setRepeated(true);
                    r.getAllCh()[v].setWhoNext(j);
                }
              
                int l = r.getAllCh().FindLastIndex(v ,
                     delegate (Node n)
                     {
                         return n.getName() == m.getAllCh()[v].getName();
                     });

                if (j > 0 && l > 0 && v == l + 1)
                {
                    r.getAllCh()[v].setIsFirst(true);
                    r.getAllCh()[l].setIsLast(true);
                }
                if (j != v && j >= 0 && !repeat1.Contains(j) && !repeat1.Contains(v))
                {
                    // e.Enqueue(v);
                    repeat1.Add(v);
                    // e.Enqueue(j);
                    repeat1.Add(j);
                 //   r.getAllCh()[v].setWhoNext(j);

                }

            }
            if (r.getCountCh() > 0&&repeat1.Count>0)
            {
                int f = repeat1[0];
                r.getAllCh()[repeat1[0]].setIsFirst(true);
                r.getAllCh()[repeat1[repeat1.Count - 1]].setIsLast(true);
            }
            if (!r.getRepeated())
            {
                for (int t = 0; t < depth; t++)
                    json = json + "\t";
                json = json + "\"" + $"{r.getName()}\": " + "\n";
            }

            if (r.getIsFirst())
            {

                for (int t = 0; t < depth; t++)
                    json = json + "\t";
                json = json + "[" + "\n";
            }
            for (int t = 0; t < depth; t++)
                json = json + "\t";
            json = json + "{";
            if (r.getOneLine() && r.getAllAttr().Count == 0)
            {
                json = json + "null" + "\n";
            }
            int cAttr = r.getAllAttr().Count;
            while (cAttr != 0)
            {

                enter = true;

                if (i % 2 == 0)

                {
                    json = json + "\n";
                    for (int t = 0; t < depth; t++)
                        json = json + "\t";
                    json = json + $"@{r.getOneAttr()}: ";
                }
                else
                    json = json + $"{r.getOneAttr()}";
                i++;
                cAttr--;
            }
            
            if (enter)
            {
                json = json + "\n";
                for (int t = 0; t < depth; t++)
                    json = json + "\t";

                json = json + "#text: " + $"\"{r.getValue() }\"" + "\n";
                for (int t = 0; t < depth; t++)
                    json = json + "\t";

            }
            else
            {
                json = json + "\n";
                for (int t = 0; t < depth; t++)
                    json = json + "\t";
                json = json + $"\"{r.getValue() }\"" + "\n";
                for (int t = 0; t < depth; t++)
                    json = json + "\t";

            }
            if (r.getRepeated())
            {
                if (!r.getIsLast()&&!root.getAllCh().Contains(r))
                {
                    json = json + "},\n";
                    // e.Dequeue();
                    if (!(r.getCountCh() > 0))
                        return;
                }
                else
                {
                    if (r.getIsLast())
                    {
                        json = json + "}\n";
                        for (int t = 0; t < depth; t++)
                            json = json + "\t";
                        json = json + "]" + "\n";
                       // e.Dequeue();
                       if(!(r.getCountCh()>0))
                        return;
                    }
                }
            }


            if (r.getCountCh() == 0)
            {
                json = json + "}\n";
                return;
            }


            for (int j = 0; j < r.getCountCh(); j++)
            {
                ////if (r.getRepeated() && !hasCh)
                //if (r.getIsTaken())
                //    continue;
                //////if(r.getCountCh()<50)
                ////if (tempRe.Count >= 1)
                ////{
                ////    repeat = tempRe;
                ////    // e = temQu;
                ////}
                //if (hasCh)
                //{
                //    tempRe = repeat;
                //    repeat = new List<int>();
                //    temQu = e;
                //    e = new Queue<int>();

                //}
                //else {
                //    if (tempRe.Count >= 1 && temQu.Count >= 1)
                //    {
                //        repeat = tempRe;
                //         e = temQu;
                //    }
                //}
                // Node y = r.getAllCh()[e.Peek()];
                //    conv2Json(ref x, ref e, e.Peek(), true);
                Node x;
                Node s;
                bool what;
                int inde;
                //firstThinkwithorderalreadyExist x = r.getAllCh()[j];
                //////if (r.getWhoNext() >= 0)
                //////{
                //////    tempIndex = j;
                //////    j = r.getWhoNext();
                //////    /////48al mn 8er repeat  x = r.getAllCh()[r.getWhoNext()];
                //////}
                //////else
                    x = r.getAllCh()[j];
             
                x.setIsTaken(true);
                what = x.getRepeated();
                inde = j;
                //if (r.getIsLast())
                //{
                //    j =tempIndex;
                //}
                ////if (||(e.Count != 0))
                ////{
                ////    x = r.getAllCh()[e.Peek()];

                ////    what = true;
                ////    inde = e.Peek();
                ////    if (!equal)
                ////    {
                ////        j--;
                ////    }
                ////    if (x.getCountCh() > 0)
                ////        hasCh = true;
                ////    else
                ////        hasCh = false;

                ////}
                ////else
                ////{

                ////    x = r.getAllCh()[j];

                ////    ////if ((x.getCountCh() >= 1))
                ////    ////{
                ////    ////    tempRe = repeat;
                ////    ////    repeat = new List<int>();
                ////    ////    //temQu = e;
                ////    ////    //e = new Queue<int>();

                ////    ////}

                ////    what = false;
                ////    inde = j;
                ////    ////if (x.getCountCh() > 0)
                ////    ////    hasCh = true;
                ////    ////else
                ////    ////    hasCh = false;
                ////}

                /*                Node right;
                                Node left;
                                if (j+1 <r.getAllCh().Count)
                                     right=  r.getAllCh()[j + 1];
                                if (j - 1 > 0)
                                    left = r.getAllCh()[j - 1];
                   */                 //   if(getDepth(ref x)==0&& r.getCountCh() >= 1)
                                      //       if (j - 1 > 0&& j + 1 < r.getAllCh().Count&&(getDepth(ref x)== getDepth(ref right) || getDepth(ref x) == getDepth(ref left)))
                if (x.getCountCh() >= 1)
                    depTemp = depth;
                // depTemp = getDepth(ref root) - getDepth(ref x);
                depth = (x.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x) : depTemp + 1;
             ////   conv2Json(ref x, ref e, ref repeat, inde, what, depth, ref json, hasCh);
                conv2Json(ref x,inde, what,depth,ref json);
            }
            if (r.getRepeated())
            {
                if (!r.getIsLast())
                {
                    for (int t = 0; t < depth-1; t++)
                        json = json + "\t";
                    json = json + "},\n";
                    // e.Dequeue();
                    if (!(r.getCountCh() > 0))
                        return;
                }
                else
                {
                    if (r.getIsLast())
                    {
                        json = json + "}\n";
                        for (int t = 0; t < depth; t++)
                            json = json + "\t";
                        json = json + "]" + "\n";
                        // e.Dequeue();
                        if (!(r.getCountCh() > 0))
                            return;
                    }
                }
            }
            if(r==root)
                json = json + "}\n";


        }
        //public void conv2Json(ref Node r, ref Queue<int> e, ref List<int> repeat, int ind, bool equal, int depth, ref string json,bool hasCh)
        ////public void conv2Json(ref Node r, int ind, bool equal, int depth, ref string json)

        //{

        //    //    Queue<int> e;
        //    //    e = new Queue<int>();
        //    //List<int> repeat= new List<int>();
        //    int i = 0;
        //    bool enter = false;
        //    //    
        //    Node m = r;
        //    for (int v = 0; v < r.getCountCh(); v++)
        //    {
        //        int j = r.getAllCh().FindIndex(v + 1,
        //             delegate (Node n)
        //             {
        //                 return n.getName() == m.getAllCh()[v].getName();
        //             });
        //        if (j != v && j >= 0 && !e.Contains(j) && !e.Contains(v))
        //        {
        //            e.Enqueue(v);
        //            repeat.Add(v);
        //            e.Enqueue(j);
        //            repeat.Add(j);

        //        }

        //    }
        //    if (!repeat.Contains(ind) || ind == repeat[0])
        //    {
        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";
        //        json = json + "\"" + $"{r.getName()}\": " + "\n";
        //    }

        //    if (e.Count != 0 && repeat.Count > 0 && ind == repeat[0])
        //    {

        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";
        //        json = json + "[" + "\n";
        //    }
        //    for (int t = 0; t < depth; t++)
        //        json = json + "\t";
        //    json = json + "{";
        //    if (r.getOneLine() && r.getAllAttr().Count == 0)
        //    {
        //        json = json + "null" + "\n";
        //    }
        //    int cAttr = r.getAllAttr().Count;
        //    while (cAttr != 0)
        //    {

        //        enter = true;

        //        if (i % 2 == 0)

        //        {
        //            json = json + "\n";
        //            for (int t = 0; t < depth; t++)
        //                json = json + "\t";
        //            json = json + $"@{r.getOneAttr()}: ";
        //        }
        //        else
        //            json = json + $"{r.getOneAttr()}";
        //        i++;
        //        cAttr--;
        //    }

        //    if (enter)
        //    {
        //        json = json + "\n";
        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";

        //        json = json + "#text: " + $"\"{r.getValue() }\"" + "\n";
        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";

        //    }
        //    else
        //    {
        //        json = json + "\n";
        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";
        //        json = json + $"\"{r.getValue() }\"" + "\n";
        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";

        //    }
        //    if (e.Count != 0 && e.Contains(ind))
        //    {
        //        if (repeat[repeat.Count - 1] != e.Peek())
        //        {
        //            json = json + "},\n";
        //            e.Dequeue();
        //            return;
        //        }
        //        else
        //        {
        //            if (!hasCh)
        //            {
        //                json = json + "}\n";
        //                for (int t = 0; t < depth; t++)
        //                    json = json + "\t";
        //                json = json + "]" + "\n";
        //                e.Dequeue();
        //                return;
        //            }
        //        }
        //    }


        //    if (r.getCountCh() == 0)
        //    {
        //        json = json + "}\n";
        //        return;
        //    }


        //    for (int j = 0; j < r.getCountCh(); j++)
        //    {
        //        if (repeat.Contains(j)&&!hasCh)
        //            continue;
        //        //if(r.getCountCh()<50)
        //        if (tempRe.Count >= 1)
        //        {
        //            repeat = tempRe;
        //           // e = temQu;
        //        }
        //        //if (hasCh)
        //        //{
        //        //    tempRe = repeat;
        //        //    repeat = new List<int>();
        //        //    temQu = e;
        //        //    e = new Queue<int>();

        //        //}
        //        //else {
        //        //    if (tempRe.Count >= 1 && temQu.Count >= 1)
        //        //    {
        //        //        repeat = tempRe;
        //        //         e = temQu;
        //        //    }
        //        //}
        //        // Node y = r.getAllCh()[e.Peek()];
        //        //    conv2Json(ref x, ref e, e.Peek(), true);
        //        Node x;
        //        Node s;
        //        bool what;
        //        int inde;

        //        if ((e.Count != 0))
        //        {
        //            x = r.getAllCh()[e.Peek()];

        //            what = true;
        //            inde = e.Peek();
        //            if (!equal)
        //            {
        //                j--;
        //            }
        //            if (x.getCountCh() > 0)
        //                hasCh = true;
        //            else
        //                hasCh = false;

        //        }
        //        else
        //        {

        //            x = r.getAllCh()[j];

        //            if (( x.getCountCh() >= 1))
        //            {
        //                tempRe = repeat;
        //                repeat = new List<int>();
        //                //temQu = e;
        //                //e = new Queue<int>();

        //            }

        //            what = false;
        //            inde = j;
        //            if (x.getCountCh() > 0)
        //                hasCh = true;
        //            else
        //                hasCh = false;
        //        }

        //        /*                Node right;
        //                        Node left;
        //                        if (j+1 <r.getAllCh().Count)
        //                             right=  r.getAllCh()[j + 1];
        //                        if (j - 1 > 0)
        //                            left = r.getAllCh()[j - 1];
        //           */                 //   if(getDepth(ref x)==0&& r.getCountCh() >= 1)
        //                              //       if (j - 1 > 0&& j + 1 < r.getAllCh().Count&&(getDepth(ref x)== getDepth(ref right) || getDepth(ref x) == getDepth(ref left)))
        //        if (x.getCountCh() >= 1)
        //            depTemp = depth;
        //        // depTemp = getDepth(ref root) - getDepth(ref x);
        //        depth = (x.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x) : depTemp + 1;
        //        conv2Json(ref x, ref e, ref repeat, inde, what, depth, ref json,hasCh);
        //        //conv2Json(ref x,inde, what,depth,ref json);
        //    }
        //    json = json + "}\n";


        //}
        ////  public void conv2Json(ref Node r, ref Queue<int> e, ref List<int> repeat, int ind, bool equal, int depth, ref string json, ref Queue<Node> childreen, bool hasCh)
        //   public void conv2Json(ref Node r, ref List<int> repeat, int ind, bool equal, int depth, ref string json)

        //  {

        //        Queue<int> e = new Queue<int>();
        //      //List<int> repeat= new List<int>();
        //      int i = 0;
        //      bool enter = false;
        //      Node m = r;
        //      for (int v = 0; v < r.getCountCh(); v++)
        //      {
        //          int j = r.getAllCh().FindIndex(v + 1,
        //               delegate (Node n)
        //               {
        //                   return n.getName() == m.getAllCh()[v].getName();
        //               });
        //          if (j != v && j >= 0 && !e.Contains(j) && !e.Contains(v))
        //          {
        //              e.Enqueue(v);
        //              repeat.Add(v);
        //              e.Enqueue(j);
        //              repeat.Add(j);

        //          }
        //      }
        //      if (!repeat.Contains(ind) || ind == repeat[0])
        //      {
        //          for (int t = 0; t < depth; t++)
        //              json = json + "\t";
        //          json = json + "\"" + $"{r.getName()}\": " + "\n";
        //      }

        //      if (e.Count != 0 && repeat.Count > 0 && ind == repeat[0])
        //      {

        //          for (int t = 0; t < depth; t++)
        //              json = json + "\t";
        //          json = json + "[" + "\n";
        //      }
        //      for (int t = 0; t < depth; t++)
        //          json = json + "\t";
        //      json = json + "{";
        //      if (r.getOneLine() && r.getAllAttr().Count == 0)
        //      {
        //          json = json + "null" + "\n";
        //      }
        //      int cAttr = r.getAllAttr().Count;
        //      while (cAttr != 0)
        //      {

        //          enter = true;

        //          if (i % 2 == 0)

        //          {
        //              json = json + "\n";
        //              for (int t = 0; t < depth; t++)
        //                  json = json + "\t";
        //              json = json + $"@{r.getOneAttr()}: ";
        //          }
        //          else
        //              json = json + $"{r.getOneAttr()}";
        //          i++;
        //          cAttr--;
        //      }

        //      if (enter)
        //      {
        //          json = json + "\n";
        //          for (int t = 0; t < depth; t++)
        //              json = json + "\t";

        //          json = json + "#text: " + $"\"{r.getValue() }\"" + "\n";
        //          for (int t = 0; t < depth; t++)
        //              json = json + "\t";

        //      }
        //      else
        //      {
        //          json = json + "\n";
        //          for (int t = 0; t < depth; t++)
        //              json = json + "\t";
        //          json = json + $"\"{r.getValue() }\"" + "\n";
        //          for (int t = 0; t < depth; t++)
        //              json = json + "\t";

        //      }
        //      if (e.Count != 0 && e.Contains(ind))
        //      {
        //          if (repeat[repeat.Count - 1] != e.Peek())
        //          {
        //              json = json + "},\n";
        //              e.Dequeue();
        //              //  return;
        //          }
        //          else
        //          {
        //              json = json + "}\n";
        //              for (int t = 0; t < depth; t++)
        //                  json = json + "\t";
        //              json = json + "]" + "\n";
        //              e.Dequeue();

        //              //     return;
        //          }
        //      }


        //      if (r.getCountCh() == 0)
        //      {
        //          json = json + "}\n";
        //          //  return;
        //      }


        //          //if (hasCh && childreen.Count > 0)
        //          //{

        //          //    // Node y = r.getAllCh()[e.Peek()];
        //          //    //    conv2Json(ref x, ref e, e.Peek(), true);
        //          //    Node x;
        //          //    Node s;
        //          //    bool what;
        //          //    int inde = 0;
        //          //    while (childreen.Count != 0)
        //          //    {
        //          //        //if (tempRe.Count >= 1)
        //          //        //{
        //          //        //    repeat = tempRe;
        //          //        //    //e = temQu;
        //          //        //}
        //          //        //if (v != 0 && repeat.Contains(v))
        //          //        //    continue;
        //          //        //if ((e.Count != 0))
        //          //        //{
        //          //        //    x = r.getAllCh()[e.Peek()];

        //          //        //    what = true;
        //          //        //    inde = e.Peek();
        //          //        //    if (!equal)
        //          //        //    {
        //          //        //        j--;
        //          //        //    }
        //          //        //}
        //          //        //List<Node> l = new List<Node>();
        //          //        //s = childreen[v];
        //          //        //inde = v;
        //          //        List<Node> l = new List<Node>();
        //          //        s = childreen.Peek();
        //          //        childreen.Dequeue();
        //          //        inde++;
        //          //        what = false;
        //          //        conv2Json(ref s, ref e, ref repeat, inde, what, depth, ref json, ref childreen, hasCh);

        //          //    }
        //          //    return;
        //          //}
        //          for (int j = 0; j < r.getCountCh(); j++)
        //      {
        //          //if(r.getCountCh()<50)
        //          if (tempRe.Count >= 1)
        //          {
        //              repeat = tempRe;
        //              //e = temQu;
        //          }
        //          if (j != 0 && repeat.Contains(j))
        //              continue;
        //          Node x;
        //          Node s;
        //          bool what;
        //          int inde;
        //          if ((e.Count != 0))
        //          {
        //              x = r.getAllCh()[e.Peek()];

        //              what = true;
        //              inde = e.Peek();
        //              if (!equal)
        //              {
        //                  j--;
        //              }
        //              //if (x.getCountCh() > 0)
        //              //{
        //              //    hasCh = true;
        //              //    for (int c = 0; c < x.getCountCh(); c++)
        //              //        childreen.Enqueue(x.getAllCh()[c]);
        //              //    //tempRe = repeat;
        //              //    //repeat = new List<int>();
        //              //    //temQu = e;
        //              //    //e = new Queue<int>();
        //              //    if (hasCh && childreen.Count > 0)
        //              //    {
        //              //        tempRe = repeat;
        //              //        repeat = new List<int>();
        //              //        temQu = e;
        //              //        e = new Queue<int>();
        //              //    }

        //              //}
        //              //else
        //              //{
        //              //    hasCh = false;
        //              //    childreen = new Queue<Node>();
        //              //    if (tempRe.Count >= 1 && temQu.Count >= 1)
        //              //    {
        //              //        repeat = tempRe;
        //              //        e = temQu;
        //              //    }
        //              //}
        //          }

        //          else
        //          {

        //              x = r.getAllCh()[j];

        //              if (x.getCountCh() >= 1)
        //              {
        //                  tempRe = repeat;
        //                  repeat = new List<int>();
        //                  //temQu = e;
        //                  //e = new Queue<int>();

        //              }

        //              what = false;
        //              inde = j;
        //          }

        //          if (x.getCountCh() >= 1)
        //              depTemp = depth;

        //          // depTemp = getDepth(ref root) - getDepth(ref x);
        //          depth = (x.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x) : depTemp + 1;
        //          conv2Json(ref x, ref e, ref repeat, inde, what, depth, ref json, ref childreen, hasCh);
        //         // conv2Json(ref x, ref repeat, inde, what, depth, ref json);
        //          //if (hasCh && childreen.Count > 0)
        //          //{

        //          //    inde = 0;
        //          //    while (childreen.Count != 0)
        //          //    {
        //          //        //        if (x.getCountCh() >= 1)
        //          //        //        {
        //          //        //            tempRe = repeat;
        //          //        //            repeat = new List<int>();
        //          //        //            temQu = e;
        //          //        //            e = new Queue<int>();

        //          //        //        }
        //          //        s = childreen.Peek();
        //          //        childreen.Dequeue();
        //          //        inde++;
        //          //        what = false;
        //          //        conv2Json(ref s, ref e, ref repeat, inde, what, depth, ref json, ref childreen, hasCh);

        //          //    }
        //          //    return;
        //          //}
        //      }
        //      json = json + "}\n";


        //  }
        //    public void conv2Json(ref Node r, ref Queue<int> e, ref List<int> repeat, int ind, bool equal, int depth, ref string json, ref Queue<Node> childreen,bool hasCh )
        /////    public void conv2Json(ref Node r, ref List<int> repeat, int ind, bool equal, int depth, ref string json)

        //    {

        //         //   Queue<int> e = new Queue<int>();
        //        //List<int> repeat= new List<int>();
        //        int i = 0;
        //        bool enter = false;
        //        //       depTemp = depth;          
        //        //    depth = (r.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref r) : getDepth(ref root) - getDepth(ref r) + 1;
        //        //  depth = getDepthNode(ref root, ref r);
        //        //    r.getAllCh().Sort();
        //        //if(r.getCountCh()<50)
        //        //  for (int j = 0; j < r.getCountCh(); j++)
        //        //      for (int v = 0; v < r.getCountCh(); v++)
        //        //      {
        //        //          if (j != v && r.getAllCh()[j].getName().Equals(r.getAllCh()[v].getName()) && !e.Contains(j))
        //        //           //   e.AddLast(j);
        //        //         // if (r.getAllCh().BinarySearch())
        //        //          {
        //        //      //        r.getAllCh().BinarySearch(r.getAllCh()[v]);
        //        //              e.Enqueue(j);
        //        //              repeat.Add(j);

        //        //          }
        //        //      }
        //        //  if (r.getCountCh() < 50)
        //        //  for (int j = 0; j < r.getCountCh(); j++)
        //        Node m = r;
        //        for (int v = 0; v < r.getCountCh(); v++)
        //                {
        //            // if (j != v && r.getAllCh()[j].getName().Equals(r.getAllCh()[v].getName()) && !e.Contains(j))
        //            //   e.AddLast(j);
        //            // if (r.getAllCh().BinarySearch())
        //            // {
        //            //        r.getAllCh().BinarySearch(r.getAllCh()[v]);
        //           int j =  r.getAllCh().FindIndex(v+1,
        //                delegate (Node n)
        //                {
        //                    return n.getName() == m.getAllCh()[v].getName();
        //                });
        //            if (j != v && j >= 0 && !e.Contains(j) && !e.Contains(v))
        //            {
        //                e.Enqueue(v);
        //                repeat.Add(v);
        //                e.Enqueue(j);
        //                repeat.Add(j);

        //            }
        //          //  e.Dequeue();
        //                   // }
        //                }
        //        /*  Node m = r;
        //            List<Node> nodes;
        //            for (int v = 0; v < r.getCountCh(); v++)
        //                {
        //               List<Node> nodes = r.getAllCh().FindAll(
        //                delegate (Node n)
        //                {
        //                    return  n.getName() == m.getAllCh()[v].getName();

        //                }
        //                );
        //            }*/
        //        //for(int v =0;v<)
        //        if (!repeat.Contains(ind) || ind == repeat[0])
        //        { for (int t = 0; t < depth; t++)
        //                json = json + "\t";
        //            json = json +"\"" +$"{r.getName()}\": " + "\n";
        //        }

        //        if (e.Count != 0  &&repeat.Count>0 &&ind == repeat[0])
        //        {

        //            for (int t = 0; t < depth; t++)
        //                json = json + "\t";
        //            json = json + "["+"\n";
        //        }
        //        for (int t = 0; t < depth; t++)
        //            json = json + "\t";
        //        json = json + "{";
        //        if (r.getOneLine()&&r.getAllAttr().Count==0)
        //        {
        //            json = json + "null"+"\n";
        //        }
        //        int cAttr = r.getAllAttr().Count;
        //        while (cAttr != 0)
        //        {

        //            enter = true;

        //            if (i % 2 == 0)

        //            {
        //                json = json + "\n";
        //                for (int t = 0; t < depth; t++)
        //                    json = json + "\t";
        //                json = json + $"@{r.getOneAttr()}: ";
        //            }
        //            else
        //                json = json + $"{r.getOneAttr()}";
        //            i++;
        //            cAttr--;
        //        }

        //        if (enter)
        //        {
        //            json = json + "\n";
        //            for (int t = 0; t < depth; t++)
        //                json = json + "\t";

        //            json = json + "#text: " + $"\"{r.getValue() }\"" + "\n";
        //            for (int t = 0; t < depth; t++)
        //                json = json + "\t";

        //        }
        //        else
        //        {
        //            json = json + "\n";
        //            for (int t = 0; t < depth; t++)
        //                json = json + "\t";
        //            json = json + $"\"{r.getValue() }\"" + "\n";
        //            for (int t = 0; t < depth; t++)
        //                json = json + "\t";

        //        }
        //        if (e.Count != 0  && e.Contains(ind))
        //        {
        //            if (repeat[repeat.Count - 1] != e.Peek())
        //            {
        //                json = json + "},\n";
        //                e.Dequeue();
        //              //  return;
        //            }
        //            else
        //            {
        //                json = json + "}\n";
        //                for (int t = 0; t < depth; t++)
        //                    json = json + "\t";
        //                json = json + "]"+"\n";
        //                e.Dequeue();

        //            //     return;
        //            }
        //        }


        //        if (r.getCountCh() == 0)
        //        {
        //            json = json + "}\n";
        //          //  return;
        //        }


        //        //if (hasCh && childreen.Count > 0)
        //        //{

        //        //    // Node y = r.getAllCh()[e.Peek()];
        //        //    //    conv2Json(ref x, ref e, e.Peek(), true);
        //        //    Node x;
        //        //    Node s;
        //        //    bool what;
        //        //    int inde = 0;
        //        //    while(childreen.Count!=0)
        //        //    {
        //        //        //if (tempRe.Count >= 1)
        //        //        //{
        //        //        //    repeat = tempRe;
        //        //        //    //e = temQu;
        //        //        //}
        //        //        //if (v != 0 && repeat.Contains(v))
        //        //        //    continue;
        //        //        //if ((e.Count != 0))
        //        //        //{
        //        //        //    x = r.getAllCh()[e.Peek()];

        //        //        //    what = true;
        //        //        //    inde = e.Peek();
        //        //        //    if (!equal)
        //        //        //    {
        //        //        //        j--;
        //        //        //    }
        //        //        //}
        //        //        //List<Node> l = new List<Node>();
        //        //        //s = childreen[v];
        //        //        //inde = v;
        //        //        List<Node> l = new List<Node>();
        //        //        s = childreen.Peek();
        //        //        childreen.Dequeue();
        //        //        inde++;
        //        //        what = false;
        //        //        conv2Json(ref s, ref e, ref repeat, inde, what, depth, ref json, ref childreen,hasCh);

        //        //    }
        //        //    return;
        //        //}
        //        for (int j = 0; j < r.getCountCh(); j++)
        //        {
        //            //if(r.getCountCh()<50)
        //            if (tempRe.Count >= 1)
        //            { repeat = tempRe;
        //                e = temQu;
        //            }
        //            if (j != 0 && repeat.Contains(j))
        //                continue;
        //            // Node y = r.getAllCh()[e.Peek()];
        //            //    conv2Json(ref x, ref e, e.Peek(), true);
        //            Node x;
        //            Node s;
        //            bool what;
        //            int inde;
        //            //if ((e.Count != 0) && r.getCountCh() > 0)
        //            //{
        //            //    // depTemp = getDepth(ref root) - getDepth(ref x);

        //            //    x = r.getAllCh()[j];
        //            //    inde = j;
        //            //    what = false;
        //            //    //if (s.getCountCh() >= 1)
        //            //    //{
        //            //    //    tempRe = repeat;
        //            //    //    repeat = new List<int>();

        //            //    //}

        //            //    //if (s.getCountCh() >= 1)
        //            //    //    depTemp = depth;

        //            //    // depTemp = getDepth(ref root) - getDepth(ref x);
        //            //    //depth = (s.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x) : depTemp + 1;

        //            //    //conv2Json(ref s, ref e, ref repeat, inde, what, depth, ref json);
        //            //}

        //            //   else
        //            if ((e.Count != 0))
        //            {
        //                x = r.getAllCh()[e.Peek()];

        //                what = true;
        //                inde = e.Peek();
        //                if (!equal)
        //                {
        //                    j--;
        //                }
        //                if (x.getCountCh() > 0)
        //                {
        //                    hasCh = true;
        //                    for(int c = 0;c< x.getCountCh();c++)
        //                        childreen .Enqueue(x.getAllCh()[c]);
        //                    //// Node y = r.getAllCh()[e.Peek()];
        //                    ////    conv2Json(ref x, ref e, e.Peek(), true);

        //                    //    if (x.getCountCh() >= 1)
        //                    //        depTemp = depth;
        //                    //    // depTemp = getDepth(ref root) - getDepth(ref x);
        //                    //    depth = (x.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x) : depTemp + 1;
        //                    //    conv2Json(ref x, ref e, ref repeat, inde, what, depth, ref json, ref r);

        //                    //    s = x.getAllCh()[0];
        //                    //    inde = 0;
        //                    //    what = false;
        //                    //    conv2Json(ref s, ref e, ref repeat, inde, what, depth, ref json, ref r,hasCh);

        //                }
        //                else
        //                {
        //                    hasCh = false;
        //                   childreen = new Queue<Node>();

        //                }
        //            }

        //            else
        //            {

        //                x = r.getAllCh()[j];

        //                if (x.getCountCh() >= 1)
        //                {
        //                    tempRe = repeat;
        //                    repeat = new List<int>();
        //                    //temQu = e;
        //                    //e = new Queue<int>();

        //                }

        //                what = false;
        //                inde = j;
        //            }

        //            /*                Node right;
        //                            Node left;
        //                            if (j+1 <r.getAllCh().Count)
        //                                 right=  r.getAllCh()[j + 1];
        //                            if (j - 1 > 0)
        //                                left = r.getAllCh()[j - 1];
        //               */                 //   if(getDepth(ref x)==0&& r.getCountCh() >= 1)
        //                                  //       if (j - 1 > 0&& j + 1 < r.getAllCh().Count&&(getDepth(ref x)== getDepth(ref right) || getDepth(ref x) == getDepth(ref left)))
        //            if (x.getCountCh() >= 1)
        //            depTemp = depth ;

        //            // depTemp = getDepth(ref root) - getDepth(ref x);
        //            depth = (x.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x)  :  depTemp + 1;
        //            conv2Json(ref x, ref e, ref repeat, inde, what, depth, ref json, ref childreen,hasCh);
        //            //    conv2Json(ref x,ref repeat,inde, what,depth,ref json);
        //            if (hasCh && childreen.Count > 0)
        //            {

        //                // Node y = r.getAllCh()[e.Peek()];
        //                //    conv2Json(ref x, ref e, e.Peek(), true);
        //                //Node x;
        //                //Node s;
        //                //bool what;
        //                 inde = 0;
        //                while (childreen.Count != 0)
        //                {
        //                    //if (tempRe.Count >= 1)
        //                    //{
        //                    //    repeat = tempRe;
        //                    //    //e = temQu;
        //                    //}
        //                    //if (v != 0 && repeat.Contains(v))
        //                    //    continue;
        //                    //if ((e.Count != 0))
        //                    //{
        //                    //    x = r.getAllCh()[e.Peek()];

        //                    //    what = true;
        //                    //    inde = e.Peek();
        //                    //    if (!equal)
        //                    //    {
        //                    //        j--;
        //                    //    }
        //                    //}
        //                    //List<Node> l = new List<Node>();
        //                    //s = childreen[v];
        //                    //inde = v;
        //                    //       List<Node> l = new List<Node>();
        //                //if(!e.Count==0)
        //                    if (x.getCountCh() >= 1)
        //                    {
        //                        tempRe = repeat;
        //                        repeat = new List<int>();
        //                        temQu = e;
        //                        e = new Queue<int>();

        //                    }
        //                    s = childreen.Peek();
        //                    childreen.Dequeue();
        //                    inde++;
        //                    what = false;
        //                    conv2Json(ref s, ref e, ref repeat, inde, what, depth, ref json, ref childreen, hasCh);

        //                }
        //                return;
        //            }
        //        }
        //        json = json + "}\n";


        //    } 
        public string getJSON()
        {
            return json0;
        }
    }
 }
