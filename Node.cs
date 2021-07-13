using System;
using System.Collections.Generic;
using System.Text;

namespace XML_editor
{
    class Node
    {
        private string name;
        private Queue<string> attr;
        private int index;
        private List<Node> children;
        private string value;
        public Node()
        {
            name = "";
            value = "";
            attr = new Queue<string>();
            children = new List<Node>();
            index = 0;
        }
        public Node(string n)
        {
            name = n;
            value = "";
            attr = new Queue<string>();
            children = new List<Node>();
            index = 0;
        }
        public Node(string n, string v)
        {
            name = n;
            value = v;
            attr = new Queue<string>();
            children = new List<Node>();
            index = 0;
        }
        public Node(string n,ref Queue<string> att,ref List<Node> ch, string v)
        {
            name = n;
            attr = att;
            children = ch;
            value = v;
            index = 0;
        }
        
        public void setName(string n)
        {
            name = n ;
        }
        public void setOneAttr(string q)
        {
            attr.Enqueue(q);
        }
        public void setchild(ref Node c)
        {
            children[index] = c;
            index++;
        }
        public string getName()
        {
            return name;
        }
        public string getOneAttr()
        {
            string x;
            if (attr.Count != 0)
            {
                x = attr.Peek();
                attr.Dequeue();
                return x;
            }
            else
                return ".-1.";
        }
        public int getchild()
        {
            int i = -1;
            if (children.Count != 0)
            {
                i = index;
                index--;
                return i;
            }
            else
                return i;
        }
        public Queue<string> getAllAttr()
        {
            return attr;
        }
        public ref List<Node> getAllCh()
        {
            return ref children;
        }
        public int getCountCh()
        {
            return children.Count;
        }
        public int getCountaAttr()
        {
            return attr.Count;
        }
        public void setValue(string input)
        {
            value = input;
        }
        public string getValue()
        {
            return value;
        }
        

    }
}
