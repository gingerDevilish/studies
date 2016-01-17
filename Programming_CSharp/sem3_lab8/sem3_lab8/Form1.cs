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
    public partial class Form1 : Form
    {

        static Int32 seconds = 0;
        static Timer timer;
        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
        }

        void button1_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            button1.Enabled = false;
            DialogResult dRes = form.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                Int32 time = Int32.Parse(form.textBox1.Text);
                seconds = time * 60;
                timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += timer_Tick;
                timer.Start();
                button1.ForeColor = Color.Red;
                

            }
            else
            {
                button1.Enabled = true;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                button1.Text = (seconds / 60).ToString("D2") + ":" + (seconds % 60).ToString("D2");
                seconds--;
            }
            else
            {
                timer.Stop();
                button1.Text = "Set Timer";
                button1.Enabled = true;
                button1.ForeColor = Color.Black;
            }

        }

    }
}
