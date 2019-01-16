using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiG
{
    public partial class Form4 : Form
    {
        Form1 form1;
        public Form4(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
            textBox1.Text = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") {
                form1.setCoef(Convert.ToInt32(textBox1.Text));
            }
        }
    }
}
