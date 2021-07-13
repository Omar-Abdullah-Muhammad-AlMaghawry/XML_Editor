using System;
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
            Node root = new Node();
            Tree tree = new Tree(ref root, richTextBox2.Text);
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
    }
}
