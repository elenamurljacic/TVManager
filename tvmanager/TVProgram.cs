using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;

namespace tvmanager
{
    public partial class TVProgram : Form
    {
        SqlConnection connection;   //pruza konekciju koju prosljeđujemo drugim objektima, a oni to koriste za čitanje baze 
        string connectionString;   //sadrzi informaciju gdje se nalazi baza i kako se spajamo na nju

        Monitor monitor = new Monitor();

        List<ITVSadrzaj> pon, uto, sri, cet, pet, sub, ned;

        public TVProgram()
        {
            InitializeComponent();
            monitor.Show();

            connectionString = ConfigurationManager.ConnectionStrings["tvmanager.Properties.Settings.TVSadrzajConnectionString"].ConnectionString;
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            ListViewItem new_item = lvTvProgram.Items.Add("12:30");
            new_item.SubItems.Add("Regionalni Dnevnik");

            for (int i = 0; i < lvTvProgram.Columns.Count; i++)
                lvTvProgram.Columns[i].Width = -2;

            
            List<Film> d = DohvatiIzBazeFilmove("------++");
            foreach(Film f in d)
            {
                ListViewItem listitem = new ListViewItem(f.Ime);
                lvTvProgram.Items.Add(listitem);
                listitem.SubItems.Add(f.Duljina.ToString());
            }
            txbOpis.Text = d[0].ToString();

            UnosPrograma();

        }

        private List<Film> DohvatiIzBazeFilmove(string dan)
        {
            List<Film> flm = new List<Film>();
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Film WHERE Prikazivanje LIKE '" + dan + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    flm.Add(new Film(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Redatelj"].ToString(), dr["GlavniGlumac"].ToString(), dr["Prikazivanje"].ToString()));                  
                }
            }
            return flm;
        }
        private List<Serija> DohvatiIzBazeSerije(string dan)
        {
            List<Serija> ser = new List<Serija>();
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Serija WHERE Prikazivanje LIKE '" + dan + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ser.Add(new Serija(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Redatelj"].ToString(), dr["Sezona"].ToString(), (int)dr["Epizoda"], dr["Prikazivanje"].ToString()));
                }
            }
            return ser;
        }
        private List<LivePrijenos> DohvatiIzBazeLivePrijenose(string dan)
        {
            List<LivePrijenos> liv = new List<LivePrijenos>();
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM LivePrijenos WHERE Prikazivanje LIKE '" + dan + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    liv.Add(new LivePrijenos(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Tip"].ToString(), dr["Prikazivanje"].ToString()));
                }
            }
            return liv;
        }

        private List<DSPK> DohvatiIzBazeDSPK(string dan)
        {
            List<DSPK> dsp = new List<DSPK>();
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM DSPK WHERE Prikazivanje LIKE '" + dan + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dsp.Add(new DSPK(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Tip"].ToString(), dr["Urednik"].ToString(), dr["Voditelj"].ToString(), dr["Prikazivanje"].ToString()));
                }
            }
            return dsp;
        }

        private List<Reklama> DohvatiIzBazeReklame(string dan)
        {
            List<Reklama> rek = new List<Reklama>();
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Reklama", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    rek.Add(new Reklama(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"],(int)dr["Cijena"]));
                } 
            }
            return rek;
        }

        

        private void UnosFilm(Film f)
        {
            string query = "INSERT INTO Film(Ime,Opis,Zanr,Duljina,Prioritet,DobnaSkupina,Redatelj,GlavniGlumac,Prikazivanje) VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @redatelj, @gglumac, @prikazivanje)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\TVSadrzaj.mdf;Integrated Security=True;";

               
                command.Parameters.AddWithValue("@ime", f.Ime);
                command.Parameters.AddWithValue("@opis", f.Opis);
                command.Parameters.AddWithValue("@zanr", f.Zanr);
                command.Parameters.AddWithValue("@duljina", f.Duljina);
                command.Parameters.AddWithValue("@prioritet", f.Prioritet);
                command.Parameters.AddWithValue("@dobnaskupina", f.DobnaSkupina);
                command.Parameters.AddWithValue("@redatelj", f.Redatelj);
                command.Parameters.AddWithValue("@gglumac", f.GlavniGlumac);
                command.Parameters.AddWithValue("@prikazivanje", f.Prikazivanje);

                
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ooops");
                }

            }
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
            UnosFilm(film1);

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

       

        private void lvTvProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
