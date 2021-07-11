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
        public Node(string n) {
            name = n;
            index = 0;
        }
        public Node()
        {
            index = 0;
        }
        public void setName(string n)
        {
            name = n;
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
                i=index;
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
        public List<Node> getAllCh()
        {
            return children;
        }
        public int getCountCh()
        {
            return index + 1;
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
