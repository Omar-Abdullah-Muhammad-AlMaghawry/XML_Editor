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
        private bool isOneLine = false;
        private bool repeated;
        private bool isFirst ;
        private bool isLast;
        private bool isTaken;
        private int whoNext;
        private bool isLastLast;
        private bool isFirstFirst;
        private bool isComment = false;






        public Node()
        {
            name = "";
            value = "";
            attr = new Queue<string>();
            children = new List<Node>();
            index = 0;
            repeated = false;
            isFirst = false;
            isLast = false;
            isLastLast = false;
            isFirstFirst = false;
            isTaken = false;
            whoNext = -1;
        }
        public Node(string n)
        {
            name = n;
            value = "";
            attr = new Queue<string>();
            children = new List<Node>();
            index = 0;
            repeated = false;
            isFirst = false;
            isLast = false;
            isLastLast = false;
            isFirstFirst = false;
            isTaken = false;
            whoNext = -1;
        }
        public Node(string n, string v)
        {
            name = n;
            value = v;
            attr = new Queue<string>();
            children = new List<Node>();
            index = 0;
            repeated = false;
            isFirst = false;
            isLast = false;
            isLastLast = false;
            isFirstFirst = false;
            isTaken = false;
            whoNext = -1;
        }
        public Node(string n,ref Queue<string> att,ref List<Node> ch, string v)
        {
            name = n;
            attr = att;
            children = ch;
            value = v;
            index = 0;
            repeated = false;
            isFirst = false;
            isLast = false;
            isLastLast = false;
            isFirstFirst = false;
            isTaken = false;
            whoNext = -1;
        }
        public void setOneLine(bool inp)
        {
            isOneLine = inp;
        }
        public bool getOneLine()
        {
            return isOneLine;
        
      }
        public void setIsLastLast(bool inp)
        {
            isLastLast = inp;
        }
        public bool getIsLastLast()
        {
            return isLastLast;

        }

        public void setIsFirstFirst (bool inp)
        {
            isFirstFirst = inp;
        }
        public bool getIsFirstFirst()
        {
            return isFirstFirst;

        }
        public void setWhoNext(int inp)
        {
            whoNext = inp;
        }
        public int getWhoNext()
        {
            return whoNext;

        }
        public void setRepeated(bool inp)
        {
            repeated = inp;
        }
        public bool getRepeated()
        {
            return repeated;
        }
        public void setIsFirst(bool inp)
        {
            isFirst = inp;
        }
        public bool getIsFirst()
        {
            return isFirst;
        }
        public void setIsLast(bool inp)
        {
            isLast = inp;
        }
        public bool getIsLast()
        {
            return isLast;
        }
        public void setIsTaken(bool inp)
        {
            isTaken  = inp;
        }
        public bool getIsTaken()
        {
            return isTaken;
        }
        public void setName(string n)
        {
            name = n ;
        }
        public void setOneAttr(string q)
        {
            attr.Enqueue(q);
        }
        public void setchild(Node c)
        {
            //children[index] = c;
            //index++;
            children.Add(c);
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
        public int getCountAttr()
        {
            return attr.Count;
        }
        public bool getCommentFlag()
        {
            return isComment;
        }
        public void setCommentFlag(bool val)
        {
            isComment = val;
        }
    }
}
