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
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //if (!errordetection(richTextBox2.Text))
            //{
            //    Node root = new Node();
            //    Tree tree = new Tree(ref root, richTextBox2.Text);
            //    button2.Enabled = true;
            //    button3.Enabled = true;
            //    button6.Enabled = true;
            //    button7.Enabled = true;
            //    button4.Enabled = true;
            //}
            //else
            //{
            //    button2.Enabled = false;
            //    button3.Enabled = false;
            //    button6.Enabled = false;
            //    button7.Enabled = false;
            //    button4.Enabled = false;
            //}
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
            if (!errordetection(richTextBox2.Text))
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
                tree.format();
                richTextBox1.Text = "";
                string message = "";
                for (int i = 0; i < tree.toBePrinted.Count; i++)
                {
                    message += tree.toBePrinted[i];
                }
                richTextBox1.Text = message;
            }
            else
            {
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't beutify if there are errors";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!errordetection(richTextBox2.Text))
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
                tree.Minifying();
                richTextBox1.Text = "";
                string message = "";
                for (int i = 0; i < tree.toBePrinted.Count; i++)
                {
                    //richTextBox1.Text += tree.toBePrinted[i];
                    message += tree.toBePrinted[i];
                }
                richTextBox1.Text = message;
            }
            else
            {
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't minify if there are errors";
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose the XML File";
            //      fdlg.InitialDirectory = @"d:\College\3rd Comp. and Sys\2nd Terms\Data Structure\XML_Editor_Project\data";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|XML files (*.xml*)|*.xml*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                path = fdlg.FileName;
            }
            using (FileStream xml = File.Open(path, FileMode.Open))
            {
                string data;
                byte[] b = new byte[xml.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (xml.Read(b, 0, b.Length) > 0)
                    richTextBox2.Text = temp.GetString(b);
                /*       Queue<int> er = new Queue<int>();
                       List<int> r = new List<int>();
                       tree.conv2Json(ref root, ref er, r, -1, false, 0);
                       richTextBox1.Text = tree.getJSON();*/

            }
        }

        private void compressLZW(string inputFile)
        {
            List<string> table = new List<string>();
            for (int i = 0; i < 256; i++)
            {
                table.Add(((char)i).ToString());
            }

            int code = 256;
            string p = "", c = "";
            p += inputFile[0];
            List<short> output = new List<short>();

            for (int i = 0; i < inputFile.Length; i++)
            {
                if (i != inputFile.Length - 1) c += inputFile[i + 1];
                
                Predicate<string> finderPC = delegate (string val) { return val == (p + c); };
                Predicate<string> finderP1 = delegate (string val) { return val == p; };
                if (table.FindIndex(finderPC) < table.Count && table.FindIndex(finderPC) > 0)
                {
                    p = p + c;
                }
                else
                {
                    output.Add((short)table.FindIndex(finderP1));
                    table.Add(p + c);
                    p = c;
                }
                c = "";
            }

            Predicate<string> finderP = delegate (string val) { return val == p; };
            output.Add((short)table.FindIndex(finderP));
            using (BinaryWriter binWriter = new BinaryWriter(File.Open("testfile.lzw", FileMode.Create)))
            {
                //write compressed binary to output file
                for (int i = 0; i < output.Count; i++)
                {
                    binWriter.Write(output[i]);
                }
            }

            richTextBox1.Text = "";
            string message = "";
            for (int i = 0; i < output.Count; i++)
            {
                //richTextBox1.Text += (char)output[i];
                message += (char)output[i];
            }
            richTextBox1.Text = message;
            //return output;
        }

        private string decompressLZW(List<short> code)
        {
            List<string> table = new List<string>();
            for (int i = 0; i < 256; i++)
            {
                table.Add(((char)i).ToString());
            }

            string output = "";
            int old = code[0], n = 0;
            string s = table[old];
            output += s;
            string c = "";
            c += s[0];
            int count = 256;
            for (int i = 0; i < code.Count; i++)
            {
                if (i < code.Count - 1) n = code[i + 1]; else break;
                if(n == table.Count)
                {
                    s = table[old];
                    s = s + c;
                }
                else
                {
                    s = table[n];
                }
                output += s;
                c = "";
                c += s[0];
                table.Add(table[old] + c);
                count++;
                old = n;
            }
            return output;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(richTextBox2.Text.Length > 0)
            {
                compressLZW(richTextBox2.Text);
                //string message = decompressLZW(coded);
            }
            else
            {
                richTextBox1.Text = "Please enter/browse file to be compressed";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            short test;
            List<short> coded = new List<short>(); 
            using (BinaryReader binReader = new BinaryReader(File.Open("testfile.lzw", FileMode.Open)))
            {
                while (true)
                {
                    try
                    {
                        test = binReader.ReadInt16();
                        coded.Add(test);
                    }
                    catch
                    {
                        break;
                    }
                }
            }

            richTextBox2.Text = "";
            short character;
            string message = "";
            using (BinaryReader binReader2 = new BinaryReader(File.Open("testfile.lzw", FileMode.Open)))
            {
                while (true)
                {
                    try
                    {
                        character = binReader2.ReadInt16();
                        //coded.Add(test);
                        //richTextBox2.Text += (char)character;
                        message += (char)character;
                    }
                    catch
                    {
                        break;
                    }
                }
            }

            richTextBox2.Text = message;

            if (richTextBox2.Text.Length > -1)
            {
                //List<short> coded = compressLZW(richTextBox2.Text, "testfile.lzw");
                richTextBox1.Text = decompressLZW(coded);
            }
            else
            {
                richTextBox1.Text = "Please enter/browse file to be decompressed";
            }
        }
    }
}
