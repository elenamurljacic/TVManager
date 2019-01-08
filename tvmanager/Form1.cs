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

            textBox2.Text = "HELLLLLLLLLLLLLLLOOOOOOOOO";

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            /*Film f = new Film();
            f.Ime = "sdbckbjs";
            A a = new A();
            a.s = f;

            textBox2.Text = a.s.Ime;*/

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

            DSPK Dnevnik = new DSPK("Dnevnik", "Informativna emisija", "svijet", 60, 1, 8, "dnevnik", "Goran Milic", "Petar Pereza", "-XXXXXXX");
            DSPK Vijesti = new DSPK("Vijesti", "Informativna emisija", "Split", 60, 2, 8, "dnevnik", "Goran Milic", "Petar Pereza", "-XXXXXXX");

            DSPK Prognoza = new DSPK("Prognoza", "Informativna emisija", "Hrvatska", 15, 2, 8, "vrijeme", "Goran Milic", "Petar Pereza", "-XXXXXXX");
            DSPK Sport = new DSPK("Sport", "Informativna emisija", "Svijet", 15, 2, 8, "sport", "Goran Milic", "Petar Pereza", "-XXXXXXX");

            DSPK InMagazin = new DSPK("InMagazin", "Lifestyle magazin", "Svijet", 30, 4, 8, "show", "Renata Končić", "Mia Kovačić", "-XXXXXXX");
            DSPK Potjera = new DSPK("Potjera", "Kviz", "Utrka pitanja", 90, 4, 8, "show", "Renata Končić", "Mia Kovačić", "-XXXXXXX");
            DSPK DrOz = new DSPK("DrOz", "talk show", "Zdravlje.", 60, 4, 8, "talk show", "Dr Oz", "Dr Oz", "-XXXXX--");
            DSPK DobroJutro = new DSPK("Dobro jutro Hrvatska", "jutarnja emisija", "aktualno", 120, 2, 0, "talk show", "Ana Milic", "Karmela Vukov", "-XXXXX--");

            Film film1 = new Film("Bumbleblee", "Transformeri", "akcija", 90, 3, 0, "A.K.", "", "R-----XX");
            Film film2 = new Film("Sam u kući 1", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-----XX");
            Film film3 = new Film("Sam u kući 2", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-------");
            Film film4 = new Film("Sam u kući 3", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-----XX");
            Film film5 = new Film("Sam u kući 4", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-----XX");
            Film film6 = new Film("Sam u kući 5", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-------");
            Film film7 = new Film("The help", "Istinit događaj.", "drama", 90, 3, 15, "K.E.", "P.O.", "-XXXXX--");
            Film film8 = new Film("Brzi i žestoki", "Auti.", "akcija", 90, 3, 15, "K.E.", "P.O.", "-XXXXX--");
            Film film9 = new Film("Slagalica strave", "Strah.", "horor", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--");
            Film film10 = new Film("Krug", "Strah.", "horor", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--");
            Film film11 = new Film("Teksaški masakr motornom pilom", "Strah.", "horor", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--");
            Film film12 = new Film("SuperMan1", "Strah.", "akcija", 90, 3, 12, "K.E.", "P.O.", "-XXXXX--");
            Film film13 = new Film("Harry Potter i kamen mudraca", "Čarobnjaci.", "sf", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--");

            Film animirani1 = new Film("Pokemoni", "Djeca.", "animirani", 60, 3, 0, "R.E", "Ash", "R-------");
            Film animirani2 = new Film("Teletabisi", "Djeca.", "animirani", 60, 3, 0, "R.E", "Po", "-XXXXX--");
            Film animirani3 = new Film("Pcelica Maja", "Djeca.", "animirani", 60, 3, 0, "R.E", "Maja", "R-------");
            Film animirani4 = new Film("Traktor Tom", "Djeca.", "animirani", 60, 3, 0, "R.E", "Tom", "-XXXXX--");
            Film animirani5 = new Film("Zekoslav Mrkva", "Zeko.", "animirani", 60, 3, 0, "R.E", "Zekoslav", "-XXXXX--");

            Serija serija1 = new Serija("Istanbulska nevjesta", "Turska", "drama", 60, 3, 12, "R.E", "prva", 78, "-XXXXX--");
            Serija serija2 = new Serija("Kobra", "Njemacka autocesta", "akcija", 60, 3, 12, "R.E", "treca", 94, "-XXXXX--");
            Serija serija3 = new Serija("Osveta ljubavi", "Turska", "romanticna drama", 60, 3, 12, "R.E", "prva", 124, "-XXXXX--");
            Serija serija4 = new Serija("Dadilja", "Obitelj i dadilja", "obiteljska", 60, 3, 0, "R.E", "treca", 65, "-XXXXX--");

            LivePrijenos liveprijenos1 = new LivePrijenos("The Voice", "glazbeni show", "natjecanje", 120, 3, 0, "live prijenos", "------XX");
            LivePrijenos liveprijenos2 = new LivePrijenos("Koncert", "live koncert", "glazba", 120, 2, 0, "live prijenos", "------X-");
            LivePrijenos liveprijenos3 = new LivePrijenos("Utakmica", "Hrvatska-Spanjolska", "sport", 120, 2, 0, "live prijenos", "-------X");

            Reklama reklama1 = new Reklama("Lenor", "omeksivac", "odjeca", 1, 4, 0, 45);
            Reklama reklama2 = new Reklama("Orbit", "zvaka", "prehrana", 2, 4, 0, 80);
            Reklama reklama3 = new Reklama("Karlovacko", "pivo", "prehrana", 3, 4, 0, 105);
            Reklama reklama4 = new Reklama("Ozujsko", "pivo", "prehrana", 1, 4, 0, 70);
            Reklama reklama5 = new Reklama("Lidl", "trgovina", "prehrana", 3, 4, 0, 130);
            Reklama reklama6 = new Reklama("Tommy", "trgovina", "prehrana", 2, 4, 0, 90);
            Reklama reklama7 = new Reklama("TopShop", "kupovina", "razno", 5, 4, 0, 200);
        }
    }
}
