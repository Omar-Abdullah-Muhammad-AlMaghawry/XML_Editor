using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Tree
    {
        private Node root;
        private string json ;
        
       public Tree(ref Node n)
        {
           root = n;
            json = "";
            
        }
        public void insert() { }
        public void format() { }
        public void conv2Json(ref Node r, ref Queue<int> e,ref List<int> repeat, int ind,bool equal)
        {
            //    Queue<int> eq;
            //     eq = new Queue<int>();
            //List<int> repeat = new List<int>();
            int i = 0;
            bool enter = false;
            for (int j = 0; j < r.getCountCh(); j++)
                for (int v = 0; v < r.getCountCh(); v++)
                {
                    if (j != v && r.getAllCh()[j].getName().Equals(r.getAllCh()[v].getName()) && !e.Contains(j))
                    //e.AddLast(j);
                    {
                        e.Enqueue(j);
                        repeat.Add(j);

                    }
                }
            if (!repeat.Contains(ind)||ind == repeat[0])
            json = json + $"{r.getName()}: ";
            
            if (e.Count != 0  && ind == repeat[0])
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
                json = json + "\n" + "#text: " + $"\"{r.getValue() }\""+"\n";
            else
                json = json + "\n" + $"\"{r.getValue() }\""+"\n";
         
            if (e.Count != 0  && e.Contains(ind))
            {
                if (repeat[repeat.Count - 1] != e.Peek())
                {
                    json = json + "},";
                    e.Dequeue();
                   // return;
                }
                else
                {
                    json = json + "\n}]"+"\n";
                    e.Dequeue();
                
                    //   return;
                }
            }
           
           
            if (r.getCountCh() == 0)
            {
                json = json + "\n}";
                return;
            }
           

            for (int j = 0; j < r.getCountCh(); j++)
            {
                if (repeat.Contains(j))
                    continue;
                // Node y = r.getAllCh()[e.Peek()];
                //    conv2Json(ref x, ref e, e.Peek(), true);
                Node x ;
                bool what;
                int inde;
                if ((e.Count != 0)) {
                     x = r.getAllCh()[e.Peek()];
                     what = true;
                     inde = e.Peek();
                    if (!equal)
                    {
                        j--;
                    }
                }
                else {
                     x = r.getAllCh()[j];
                     what = false;
                     inde = j;
                }
                    conv2Json(ref x, ref e, ref repeat,inde, what);
                
            }
            json = json + "}\n";
        } 
public string getJSON()
        {
            return json;
        }
    }
 }
