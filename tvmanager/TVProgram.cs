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

        Dictionary<string,ITVSadrzaj> pon, uto, sri, cet, pet, sub, ned;

        public TVProgram()
        {
            InitializeComponent();
            monitor.Show();

            connectionString = ConfigurationManager.ConnectionStrings["tvmanager.Properties.Settings.TVSadrzajConnectionString"].ConnectionString;
            
           
        }

        private List<Dictionary<string,ITVSadrzaj>> Raspored()//dan se ubacuje u obliku npr pon = '_+%'
        {
            Dictionary<string, ITVSadrzaj> pon = new Dictionary<string, ITVSadrzaj>();
            LivePrijenos vatrica = DohvatiIzBazeLivePrijenose("Vatrica"); pon.Add("00:00", vatrica);
            DSPK dbh = DohvatiIzBazeDSPK("Dobro Jutro Hrvatska"); pon.Add("06:00", dbh);
            Serija tel = DohvatiIzBazeSerije("Teletabisi"); pon.Add("08:00", tel);
            Serija pok = DohvatiIzBazeSerije("Pokemoni"); pon.Add("09:00", pok);
            Serija osv = DohvatiIzBazeSerije("Osveta Ljubavi"); pon.Add("10:00", osv);
            Serija ist = DohvatiIzBazeSerije("Istanbulska nevjesta"); pon.Add("11:00", ist);
            DSPK vij = DohvatiIzBazeDSPK("Vijesti"); pon.Add("12:00", vij);
            DSPK pro = DohvatiIzBazeDSPK("Prognoza"); pon.Add("13:00", pro);
            Serija cob = DohvatiIzBazeSerije("Kobra"); pon.Add("13:15", cob);
            Serija dad = DohvatiIzBazeSerije("Dadilja"); pon.Add("14:15", dad);
            DSPK droz = DohvatiIzBazeDSPK("DrOz"); pon.Add("15:00", droz);
            //reklama
            DSPK pot = DohvatiIzBazeDSPK("Potjera"); pon.Add("16:30", pot);
            DSPK dne = DohvatiIzBazeDSPK("Dnevnik"); pon.Add("18:00", dne);
            pon.Add("19:00", pro);
            DSPK spo = DohvatiIzBazeDSPK("Sport"); pon.Add("19:15", spo);
            DSPK inm = DohvatiIzBazeDSPK("IN Magazin"); pon.Add("19:30", inm);
            Film sup = DohvatiIzBazeFilmove("SuperMan1"); pon.Add("20:00", sup);
            Film hp = DohvatiIzBazeFilmove("Harry Potter1"); pon.Add("22:00", hp);

            Dictionary<string, ITVSadrzaj> uto = new Dictionary<string, ITVSadrzaj>(pon);


            List<Dictionary<string, ITVSadrzaj>> list = new List<Dictionary<string, ITVSadrzaj>>();
            list.Add(pon);
            return list;








        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            ListViewItem new_item = lvTvProgram.Items.Add("12:30");
            new_item.SubItems.Add("Regionalni Dnevnik");

            for (int i = 0; i < lvTvProgram.Columns.Count; i++)
                lvTvProgram.Columns[i].Width = -2;

            
            Film d = DohvatiIzBazeFilmove("Bumbleblee");
            
            
                ListViewItem listitem = new ListViewItem(d.Ime);
                lvTvProgram.Items.Add(listitem);
                listitem.SubItems.Add(d.Duljina.ToString());
            
            txbOpis.Text = d.ToString();

            //UnosPrograma();

        }



        private Film DohvatiIzBazeFilmove(string ime)
        {
            Film flm = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Film WHERE Ime LIKE '" + ime + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    flm = new Film(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Redatelj"].ToString(), dr["GlavniGlumac"].ToString(), dr["Prikazivanje"].ToString());
                }
            }
            return flm;
        }
        private Serija DohvatiIzBazeSerije(string ime)
        {
            Serija ser = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Serija WHERE Ime LIKE '" + ime + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ser = new Serija(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Redatelj"].ToString(), dr["Sezona"].ToString(), (int)dr["Epizoda"], dr["Prikazivanje"].ToString());
                }
            }
            return ser;
        }

        private void tsslPon_Click(object sender, EventArgs e)  //toolStripStatusLabel
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> pon = list[0];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach(KeyValuePair<string, ITVSadrzaj> sadrzaj in pon)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }

            /*ListViewItem new_item = lvTvProgram.Items.Add("12:30");
            new_item.SubItems.Add("Regionalni Dnevnik");

            for (int i = 0; i < lvTvProgram.Columns.Count; i++)
                lvTvProgram.Columns[i].Width = -2;

            Film d = DohvatiIzBazeFilmove("Bumbleblee");

            ListViewItem listitem = new ListViewItem(d.Ime);
            lvTvProgram.Items.Add(listitem);
            listitem.SubItems.Add(d.Duljina.ToString());*/
        }

        private void tsslUto_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> uto = list[1];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach (KeyValuePair<string, ITVSadrzaj> sadrzaj in uto)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }
        }

        private void tsslSri_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> sri = list[2];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach (KeyValuePair<string, ITVSadrzaj> sadrzaj in sri)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }
        }

        private void tsslCet_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> cet = list[3];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach (KeyValuePair<string, ITVSadrzaj> sadrzaj in cet)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }
        }

        private void tsslPet_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> pet = list[4];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach (KeyValuePair<string, ITVSadrzaj> sadrzaj in pon)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }
        }

        private void tsslSub_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> sub = list[5];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach (KeyValuePair<string, ITVSadrzaj> sadrzaj in sub)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }
        }

        private void tsslNed_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, ITVSadrzaj>> list = Raspored();
            Dictionary<string, ITVSadrzaj> ned = list[6];

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            foreach (KeyValuePair<string, ITVSadrzaj> sadrzaj in ned)
            {
                ListViewItem newItem = lvTvProgram.Items.Add(sadrzaj.Key);
                newItem.SubItems.Add(sadrzaj.Value.Ime);
            }
        }

        private LivePrijenos DohvatiIzBazeLivePrijenose(string ime)
        {
            LivePrijenos liv = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM LivePrijenos WHERE Ime LIKE '" + ime + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    liv=new LivePrijenos(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Tip"].ToString(), dr["Prikazivanje"].ToString());
                }
            }
            return liv;
        }

        private DSPK DohvatiIzBazeDSPK(string ime)
        {
            DSPK dsp = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM DSPK WHERE Prikazivanje LIKE Ime LIKE '" + ime + "'", connection)) //treba mi LIKE
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dsp = new DSPK(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Tip"].ToString(), dr["Urednik"].ToString(), dr["Voditelj"].ToString(), dr["Prikazivanje"].ToString());
                }
            }
            return dsp;
        }

        private List<Reklama> DohvatiIzBazeReklame()
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
                
                command.CommandType = CommandType.Text;

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
                finally { connection.Close(); }

            }
        }


        private void UnosPrograma()
        {
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
