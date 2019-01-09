using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tvmanager
{
    public partial class IzvanRednaSituacija : Form
    {
        public IzvanRednaSituacija()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool omoguciUnos1 = true;
            bool omoguciUnos2 = true;
            bool omoguciUnos3 = true;
            if (textBox1.Text == "") {label3.Text = "Molimo unesite naziv"; omoguciUnos1 = false; }
            if (textBox2.Text == "") { label4.Text = "Molimo unesite trajanje";omoguciUnos2 = false; }
                
            try {

                int.Parse(textBox2.Text);
            }
            catch
            {
                label4.Text = "Trajanje je u minutama!";
                textBox2.Text = "";
                omoguciUnos3 = false;
            }
            if(omoguciUnos1&&omoguciUnos2&&omoguciUnos3)
                this.Visible = false;
        }
    }
}
