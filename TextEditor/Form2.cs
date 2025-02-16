using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
        }
    }
}
