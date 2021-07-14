using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace XML_editor
{

    public partial class Form1 : Form
    {
        private Node root;
        private Tree tree;
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

            // LinkedList<int> er = new LinkedList<int>();
            Queue<int> er = new Queue<int>();
            List<int> r = new List<int>();
            Queue<string> q = new Queue<string>();
            q.Enqueue("ref");
            q.Enqueue("hhtttp **");
            Queue<string> q1 = new Queue<string>();
            q1.Enqueue("ref");
            q1.Enqueue("hhtttp **");

            List<Node> l = new List<Node>();

            l.Add(new Node("n1", "gvflgklflkg"));
            l.Add(new Node("n2", "ooooooooooooooo"));
            List<Node> l1 = new List<Node>();
            l1.Add(new Node("n10000", "mmmmmmmmmmmmmm"));
            Node v = new Node("n1", ref q1, ref l1, "dddddddvvvvvvvvvvvvvv");
            l.Add(v);
            l.Add(new Node("n3", "ddddddddddddddd"));
            l.Add(new Node("n4", "mmmmmmmmmmmmmm"));

            l.Add(new Node("n1", "mmmmmmmmmmmmmmxxxxxxxxxxxxxxxxxx"));

            Node n = new Node("span", ref q, ref l, "llllllllllllllllll");
            Tree x = new Tree(ref n);
            string json = "";
            
            bool hasCh = (n.getCountCh() > 0);
            // x.conv2Json(ref n, ref er, ref r, -1, false, 0, ref json,hasCh);
            //   x.conv2Json(ref n, -1, false, 0, ref json);
            // richTextBox1.Text = x.getJSON();
            //  string json = richTextBox1.Text;
            //   Node x = null;
            //List<Node> l = new List<Node>();
            //    Queue<Node> l = new Queue<Node>();

            //      tree.conv2Json(ref root, ref er, ref r, -1, false, 0, ref json) ;
            int d = 0;
            tree.conv2Json(ref root, -1, false,ref d, ref json);
            //richTextBox1.Text = tree.getJSON();
            richTextBox1.Text = json;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose the XML File";
            // fdlg.InitialDirectory = @"d:\College\3rd Comp. and Sys\2nd Terms\Data Structure\XML_Editor_Project\data";
            //      fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|XML files (*.xml*)|*.xml*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                path = fdlg.FileName;

                using (FileStream xml = File.Open(path, FileMode.Open))
                {
                    string data;
                    byte[] b = new byte[xml.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (xml.Read(b, 0, b.Length) > 0)
                        richTextBox2.Text = temp.GetString(b);
                    root = new Node();
                    tree = new Tree(ref root, richTextBox2.Text);
                    /*       Queue<int> er = new Queue<int>();
                           List<int> r = new List<int>();
                           tree.conv2Json(ref root, ref er, r, -1, false, 0);
                           richTextBox1.Text = tree.getJSON();*/
                    string input = richTextBox2.Text;
                    string path0 = @"mmm.lzw";
                    List<short> output = encoding(input);
                    using (BinaryWriter binWriter = new BinaryWriter(File.Open(path0, FileMode.Create)))
                    {
                        for (int i = 0; i < output.Count; i++)
                        {
                            binWriter.Write(output[i]);
                        }
                    }
                }
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public List<short> encoding(string input)
        {
            List<string> table = new List<string>();
            List<short> encFile = new List<short>();
            string p = "";
            string c = input.Substring(0,1);
            for (int i =0;i<input.Length;i++ )
            {              
                if(!table.Contains(input.Substring(i,1)))
                    table.Add(input.Substring(i, 1));
            }
            for (int i = 0; i <= input.Length; i++)
            {
               
                c = input.Substring(0, 1);
                if (!table.Contains(p+c))
                {
                    encFile.Add(short.Parse(table.IndexOf(p).ToString()));
                    table.Add(p+c);
                    p = c;
                }
                else
                {
                    p = p + c; 

                }
            }
            return encFile;
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
