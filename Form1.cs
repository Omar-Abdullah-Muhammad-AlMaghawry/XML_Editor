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
             root = new Node();
             tree = new Tree(ref root, richTextBox2.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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

              l.Add(new Node("n1","gvflgklflkg"));
              l.Add(new Node("n2", "ooooooooooooooo"));
              List<Node> l1 = new List<Node>();
             l1.Add(new Node("n10000", "mmmmmmmmmmmmmm"));
               Node v = new Node("n1", ref q1, ref l1, "dddddddvvvvvvvvvvvvvv");
                l.Add(v);
              l.Add(new Node("n3", "ddddddddddddddd"));
              l.Add(new Node("n4", "mmmmmmmmmmmmmm"));

              l.Add(new Node("n1", "mmmmmmmmmmmmmmxxxxxxxxxxxxxxxxxx"));

              Node n = new Node("span",ref q,ref l, "llllllllllllllllll");
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
              tree.conv2Json(ref root, -1, false, 0, ref json);
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
            fdlg.InitialDirectory = @"d:\College\3rd Comp. and Sys\2nd Terms\Data Structure\XML_Editor_Project\data";
      //      fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
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
                root = new Node();
                tree = new Tree(ref root, richTextBox2.Text);
         /*       Queue<int> er = new Queue<int>();
                List<int> r = new List<int>();
                tree.conv2Json(ref root, ref er, r, -1, false, 0);
                richTextBox1.Text = tree.getJSON();*/

            }
        }
    }
}
