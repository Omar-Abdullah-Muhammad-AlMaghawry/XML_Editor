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
        public void conv2JSON() { }

    }
}
