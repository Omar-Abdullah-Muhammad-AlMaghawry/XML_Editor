﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML_editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!errordetection(richTextBox2.Text))
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            errordetection(richTextBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            LinkedList<int> er = new LinkedList<int>();

            Queue<string> q = new Queue<string>();
            q.Enqueue("ref");
            q.Enqueue("hhtttp **");
            List<Node> l = new List<Node>();
            l.Add(new Node("n1","gvflgklflkg"));
            l.Add(new Node("n2", "ooooooooooooooo"));
            l.Add(new Node("n1", "ddddddddddddddd"));
            l.Add(new Node("n3", "mmmmmmmmmmmmmm"));
            Node n = new Node("span",ref q,ref l, "llllllllllllllllll");
            Tree x = new Tree(ref n);
            x.conv2Json(ref n, ref er, -1);
            richTextBox2.Text = x.getJSON();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public bool errordetection(string mystring)
        {
            richTextBox1.Text = "";
            string storee = "";
            string checkk = "";
            int counter = 1;
            bool errorr = false;
            Stack<string> myStack = new Stack<string>();
            Stack<int> counterer = new Stack<int>();
            for (int i = 0; i < mystring.Length; i++)
            {
                if (mystring[i] == '\n')
                    counter++;
                if (mystring[i] == '<')
                {
                    storee = "";
                    checkk = "";
                    i++;
                    if (mystring[i] == '/')
                    {
                        while (mystring[i] != ' ' && mystring[i + 1] != '>')
                        {
                            i++;
                            checkk += mystring[i];
                        }
                        if (myStack.Peek() == checkk)
                        {
                            myStack.Pop();
                            counterer.Pop();
                        }
                        else
                        {
                            errorr = true;
                            break;
                        }
                    }
                    else if (mystring[i] != '!' && mystring[i] != '?')
                    {
                        while (mystring[i] != ' ' && mystring[i] != '>')
                        {
                            storee += mystring[i];
                            i++;
                        }
                        while (mystring[i] != '>')
                        {
                            i++;
                        }
                        if (mystring[i - 1] != '/')
                        {
                            myStack.Push(storee);
                            counterer.Push(counter);
                        }
                    }
                }
            }
            if (errorr == true || myStack.Count != 0)
            {
                if (errorr == false)
                {
                    while (myStack.Count != 0 && counterer.Count != 0)
                    {
                        //Console.WriteLine("error occur at line " + counterer.Pop());
                        richTextBox1.Text += "Error occur at line " + counterer.Pop();
                    }
                }
                else
                {
                    //Console.WriteLine("error occur at line " + counterer.Pop());
                    richTextBox1.Text += "Error occur at line " + counterer.Pop();
                }

                return true;
            }
            else
            {
                //Console.WriteLine("no error");
                richTextBox1.Text += "No error";
                return false;
            }
            //Console.Read();
        }
    }
}
