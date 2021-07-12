using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Tree
    {
        private Node root;
        private string json = "";
        private LinkedList<int> eq;
        Tree(ref Node n)
        {
            root = n;
        }
        public void insert() { }
        public void format() { }
        public void conv2Json(ref Node r, ref LinkedList<int> e, int ind)
        {
            int i = 0;
            bool enter = false;
            if (r.getCountCh() == 0)
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
            while (r.getAllAttr().Count != -1)
            {
                enter = true;
                if (i % 2 == 0)
                    json = json + "\n" + $"@{r.getOneAttr()}: ";
                else
                    json = json + $"{r.getOneAttr()}";
                i++;
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

    }
 }
