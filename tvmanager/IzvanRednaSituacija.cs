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
        public IzvanRednaSituacija(TVProgram parentForm1) : this()
        {
            this.ParentForm1 = parentForm1;
        }
        private TVProgram ParentForm1 { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            bool omoguciUnos1 = true;
            bool omuguciUnos2 = true;
            if (textBox1.Text == "") {label3.Text = "Molimo unesite naziv"; omoguciUnos1 = false; }
            
            if(listView1.SelectedItems.Count<1)
            {
                label5.Text = "Odaberite sadržaj s kojim mjenjate izvanrednu situaciju";
                omuguciUnos2 = false;

            }
            if (omoguciUnos1 && omuguciUnos2)
            {
                string s = listView1.SelectedItems[0].Text.Substring(0, 5);
                this.ParentForm1.UpadateListBox(s, textBox1.Text);
                
                this.Close();

            }
        }

       
    }
}
