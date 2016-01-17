using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sem3_lab8
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            button1.Click += button1_click;
            textBox1.KeyPress += textBox1_KeyPress;
        }

        void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
            if (textBox1.Text.Length > 0)
                button1.Enabled = true;
            //throw new NotImplementedException();
        }

        private void button1_click(object sender, EventArgs e)
        {
            textBox1.Copy();
            

        }

    }
}
