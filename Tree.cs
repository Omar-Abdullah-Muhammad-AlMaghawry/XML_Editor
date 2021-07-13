using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Tree
    {
        private Node root;
        private string json ;
        private List<int> tempRe;
        int depTemp;


       public Tree(ref Node n)
        {
           root = n;
            json = "";
            tempRe = new List<int>();
            depTemp = 0;
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

        public void conv2Json(ref Node r, ref Queue<int> e,List<int> repeat, int ind,bool equal, int depth)
        {
            //    Queue<int> eq;
            //     eq = new Queue<int>();
            //List<int> repeat = new List<int>();
            int i = 0; 
            bool enter = false;
     //       depTemp = depth;          
        //    depth = (r.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref r) : getDepth(ref root) - getDepth(ref r) + 1;
          depth = getDepthNode(ref root, ref r);
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
            if (!repeat.Contains(ind) || ind == repeat[0])
            { for (int t = 0; t < depth; t++)
                    json = json + "\t";
                json = json + $"{r.getName()}: ";
            }
            if (e.Count != 0  && ind == repeat[0])
            {
                json = json + "[" + "\n";
            }
            for (int t = 0; t < depth; t++)
                json = json + "\t";
            json = json + "{";
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
            if (e.Count != 0  && e.Contains(ind))
            {
                if (repeat[repeat.Count - 1] != e.Peek())
                {
                    json = json + "},\n";
                    e.Dequeue();
                    return;
                }
                else
                {
                    json = json + "}\n";
                    for (int t = 0; t < depth; t++)
                        json = json + "\t";
                    json = json + "]"+"\n";
                    e.Dequeue();
                
                     return;
                }
            }
           

            if (r.getCountCh() == 0)
            {
                json = json + "}\n";
                return;
            }
           

            for (int j = 0; j < r.getCountCh(); j++)
            {
                if(tempRe.Count>=1)
                repeat = tempRe;
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
                  
                    if (x.getCountCh() >= 1)
                    {
                        tempRe = repeat;
                        repeat = new List<int>();
                       
                    }
                    
                    what = false;
                     inde = j;
                }
                /*if(j+1 <r.getAllCh().Count)
                     Node right=  r.getAllCh()[j + 1];
                if (j - 1 > 0)
                    Node left = r.getAllCh()[j - 1];*/
                    //   if(getDepth(ref x)==0&& r.getCountCh() >= 1)
   //            if (j - 1 > 0&& j + 1 < r.getAllCh().Count&&(getDepth(ref x)== getDepth(ref right)) || getDepth(ref x) == getDepth(ref left))
                 //   depTemp = depth ;
                // depTemp = getDepth(ref root) - getDepth(ref x);
               // depth = (x.getCountCh() >= 1) ? getDepth(ref root) - getDepth(ref x)  :  depTemp + 1;
                conv2Json(ref x, ref e,repeat,inde, what,depth);
            }
            json = json + "}\n";

        } 
public string getJSON()
        {
            return json;
        }
    }
 }
