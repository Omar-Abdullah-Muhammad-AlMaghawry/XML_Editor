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
        private List<short> output;
        private bool isCompres = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
            if (button2.Enabled == false && errordetection(richTextBox2.Text))
            {

                button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button4.Enabled = false;
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't do any functions if there are errors";

            }
            else
            {
                richTextBox1.Text = "";
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;
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
            if (errordetection(richTextBox2.Text))
            {

                //button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button4.Enabled = false;
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't do any functions if there are errors";

            }
            else
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;


                // LinkedList<int> er = new LinkedList<int>();
                Queue<int> er = new Queue<int>();
                List<int> r = new List<int>();
                Queue<string> q = new Queue<string>();
                //q.Enqueue("ref");
                //q.Enqueue("hhtttp **");
                //Queue<string> q1 = new Queue<string>();
                //q1.Enqueue("ref");
                //q1.Enqueue("hhtttp **");

                //List<Node> l = new List<Node>();

                //l.Add(new Node("n1", "gvflgklflkg"));
                //l.Add(new Node("n2", "ooooooooooooooo"));
                //List<Node> l1 = new List<Node>();
                //l1.Add(new Node("n10000", "mmmmmmmmmmmmmm"));
                //Node v = new Node("n1", ref q1, ref l1, "dddddddvvvvvvvvvvvvvv");
                //l.Add(v);
                //l.Add(new Node("n3", "ddddddddddddddd"));
                //l.Add(new Node("n4", "mmmmmmmmmmmmmm"));

                //l.Add(new Node("n1", "mmmmmmmmmmmmmmxxxxxxxxxxxxxxxxxx"));

                //Node n = new Node("span", ref q, ref l, "llllllllllllllllll");
                //Tree x = new Tree(ref n);

                //    bool hasCh = (n.getCountCh() > 0);
                // x.conv2Json(ref n, ref er, ref r, -1, false, 0, ref json,hasCh);
                //   x.conv2Json(ref n, -1, false, 0, ref json);
                // richTextBox1.Text = x.getJSON();
                //  string json = richTextBox1.Text;
                //   Node x = null;
                //List<Node> l = new List<Node>();
                //    Queue<Node> l = new Queue<Node>();

                //      tree.conv2Json(ref root, ref er, ref r, -1, false, 0, ref json) ;
                string json = "";
                int d = 0;
                Node father = null;
                tree.conv2Json(ref root, -1, false, ref d, ref json, ref father);
                //richTextBox1.Text = tree.getJSON();
                richTextBox1.Text = json;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (errordetection(richTextBox2.Text))
            {


                //button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button4.Enabled = false;
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't do any functions if there are errors";
            }
            else
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;

                tree.format();
                richTextBox1.Text = "";
                string outp = "";
                for (int i = 0; i < tree.toBePrinted.Count; i++)
                {
                 outp += tree.toBePrinted[i];
                }
                richTextBox1.Text = outp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (errordetection(richTextBox2.Text))
            {
                
                //button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button4.Enabled = false;
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't do any functions if there are errors";
            }
            else
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;

            
            tree.Minifying();
                string outp = "";
                richTextBox1.Text = "";
                for (int i = 0; i < tree.toBePrinted.Count; i++)
                {
                    outp += tree.toBePrinted[i];
                }
                richTextBox1.Text = outp;
            }
            
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
                    //string input = richTextBox2.Text;
                    //string path0 = @"mmm.lzw";
                    //List<short> output = encoding(input);
                    //using (BinaryWriter binWriter = new BinaryWriter(File.Open(path0, FileMode.Create)))
                    //{
                    //    for (int i = 0; i < output.Count; i++)
                    //    {
                    //        binWriter.Write(output[i]);
                    //    }
                    //}
                    //richTextBox1.Text = "";
                    //for (int i = 0; i < output.Count; i++)
                    //{
                    //    richTextBox1.Text += (byte)output[i];
                    //}
                    //List<short> output0 = new List<short>();
                    //using (BinaryReader binReader = new BinaryReader(File.Open(path0, FileMode.Open)))
                    //{

                    //    while (true)
                    //        try {
                    //            output0.Add(binReader.ReadInt16());

                    //        }
                    //        catch
                    //        {
                    //            break;
                    //        }
                    //}
                }
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public List<short> encoding_O(string input)
        {
            List<string> table = new List<string>();
            List<short> encFile = new List<short>();
            string p = "";
            string c;
     //       if (input[0].ToString()!=null)
            c = input[0].ToString();
            for (int i = 0; i < input.Length; i++)
            {
                if (!table.Contains(input.Substring(i, 1)))
                    table.Add(input[i].ToString());
            }
            for (int i = 0; i <= input.Length; i++)
            {
                if (i != input.Length)
                    c = input[i].ToString();
                else
                    c = "";
                if (!table.Contains(p + c))
                {
                    encFile.Add(short.Parse(table.IndexOf(p).ToString()));
                    table.Add(p + c);
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
            isCompres = false;
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
            using (BinaryWriter binWriter = new BinaryWriter(File.Open("test.lzw", FileMode.Create)))
            {
                //write compressed binary to output file
                for (int i = 0; i < output.Count; i++)
                {
                    binWriter.Write(output[i]);
                }
            }

            richTextBox1.Text = "";
            for (int i = 0; i < output.Count; i++)
            {
                richTextBox1.Text += (byte)output[i];
            }

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
                if (n == table.Count)
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
        private string decompressLZW_A(List<short> code)
        {
            List<string> table = new List<string>();
            string input = richTextBox2.Text;
            for (int i = 0; i < input.Length; i++)
            {
                if (!table.Contains(input.Substring(i, 1)))
                    table.Add(input[i].ToString());
            }
            string output = "";
            int old = code[0], n = 0;
            string s = table[old];
            output += s;
            string c = "";
            c += s[0];
            int count = table.Count;
            for (int i = 0; i < code.Count; i++)
            {
                if (i < code.Count - 1) n = code[i + 1]; else break;
                if (n == table.Count)
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
            if (errordetection(richTextBox2.Text))
            {
                //button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button4.Enabled = false;
                richTextBox1.Text += "\n";
                richTextBox1.Text += "Can't do any functions if there are errors";

            }
            else
            {
                Node root = new Node();
                Tree tree = new Tree(ref root, richTextBox2.Text);
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;

                if (richTextBox2.Text.Length > 0)
                {
                    // compressLZW(richTextBox2.Text);
                    //string message = decompressLZW(coded);
                    output = encoding_O(richTextBox2.Text);
                    isCompres = true;
                    string outp = "";
                    richTextBox1.Text = "";
                    for (int i = 0; i < output.Count; i++)
                    {
                        
                        outp += (byte)output[i];
                        
                    }
                    richTextBox1.Text = outp;
                }
                else
                {
                    richTextBox1.Text = "Please enter/browse file to be compressed";
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {

            string path = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose the lzw File";
            // fdlg.InitialDirectory = @"d:\College\3rd Comp. and Sys\2nd Terms\Data Structure\XML_Editor_Project\data";
            //      fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|XML files (*.xml*)|*.xml*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                path = fdlg.FileName;

               
                short test;
               // byte test0;
                List<short> coded = new List<short>();
                ////BinaryReader binReader0 = new BinaryReader(File.Open(path, FileMode.Open));
                ////while (true)
                ////{
                ////    try
                ////    {
                ////        //  test = binReader.ReadInt16();
                ////        test0 = binReader0.ReadByte();
                ////        richTextBox2.Text += (char)test0;
                ////        // coded.Add(test);
                ////    }
                ////    catch
                ////    {
                ////        break;
                ////    }
                ////}
                ////using (BinaryReader binReader = new BinaryReader(File.Open(path, FileMode.Open)))
                ////{
                ////    while (true)
                ////    {
                ////        try
                ////        {
                ////            test = binReader.ReadInt16();
                ////            coded.Add(test);
                ////        }
                ////        catch
                ////        {
                ////            break;
                ////        }
                ////    }
                ////}
                using (BinaryReader binReader1 = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    while (true)
                    {
                        try
                        {
                            test = binReader1.ReadInt16();
                            //       test0 = binReader.ReadByte();
                            //     richTextBox2.Text += (char)test0;
                            coded.Add(test);

                        }
                        catch
                        {
                            binReader1.Close();
                            break;
                        }
                    }
                }
                
                if (richTextBox2.Text.Length > -1)
                    {
                    richTextBox1.Text = decompressLZW_A(coded);
                    if (richTextBox1.Text.Substring(richTextBox1.Text.Length - 2) != ">")
                        richTextBox1.Text += ">";
                        //List<short> coded = compressLZW(richTextBox2.Text, "testfile.lzw");
                        // richTextBox1.Text = decode(coded);
                }
                    else
                    {
                        richTextBox1.Text = "Please enter/browse file to be decompressed";
                    }
                }
            }
        private void button5_Click(object sender, EventArgs e)
        {
            string path = "";
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Title = "Choose The Saving Location and The Name.extension";
             fdlg.InitialDirectory = @"d:\College\3rd Comp. and Sys\2nd Terms\Data Structure\XML_Editor_Project\data";
          //  fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|XML files (*.xml*)|*.xml*|JSON files(*.json*)|*.json*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
               
                    path = fdlg.FileName;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (isCompres)
                {
                    using (BinaryWriter binWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        //write compressed binary to output file
                        for (int i = 0; i < output.Count; i++)
                        {
                            binWriter.Write(output[i]);
                        }
                    }

                }
                else {
                    using (FileStream result = File.Create(path))
                    {
                        byte[] data = new UTF8Encoding(true).GetBytes(richTextBox1.Text);
                        result.Write(data, 0, data.Length);


                    }
                }
              
            } 
        } 
    } 
}
