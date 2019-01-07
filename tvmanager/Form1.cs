using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace tvmanager
{
    public partial class Form1 : Form
    {
        SqlConnection connection;   //pruza konekciju koju prosljeđujemo drugim objektima, a oni to koriste za čitanje baze 
        string connectionString;   //sadrzi informaciju gdje se nalazi baza i kako se spajamo na nju
        public Form1()
        {
            InitializeComponent();

            Monitor monitor = new Monitor();
            monitor.Show();

            //connectionString = ConfigurationManager.ConnectionStrings["tvmanager.Properties.Settings.TVSadrzajConnectionString"].ConnectionString;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBox2.Text = "";

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            Film f = new Film();
            f.Ime = "sdbckbjs";
            A a = new A();
            a.s = f;

            textBox2.Text = a.s.Ime;

            listView1.Columns.Add("Sat",-2,HorizontalAlignment.Left);
            listView1.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            ListViewItem new_item = listView1.Items.Add("12:30");
            new_item.SubItems.Add("Regionalni Dnevnik");

            for (int i = 0; i < listView1.Columns.Count; i++)
                listView1.Columns[i].Width = -2;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Unos_programa unos_programa = new Unos_programa();
            unos_programa.ShowDialog();
        }

        private void UnosPrograma()
        {
            /*using (connection = new SqlConnection(connectionString))
            using(SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Film", connection))
            {
                DataTable filmTable = new DataTable();
                adapter.Fill(filmTable);

                listView1.DisplayMember = "Naziv";
            }*/
            
            
        }
    }
}
