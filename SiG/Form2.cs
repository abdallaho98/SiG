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
    public partial class Form2 : Form
    {

        Interface1 interface1;
        public Form2(Interface1 interface1)
        {
            this.interface1 = interface1;
            InitializeComponent();

            comboBox1.DataSource = new List<string>() {"String","Int","Double" };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            interface1.SendData(textBox1.Text,comboBox1.Text);
        }
    }
}
