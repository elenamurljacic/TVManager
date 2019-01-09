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
        SqlConnection connection;   

        string connectionString;   

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
            Film hp = DohvatiIzBazeFilmove("Harry Potter i Kamen Mudraca"); pon.Add("22:00", hp);

            Dictionary<string, ITVSadrzaj> uto = new Dictionary<string, ITVSadrzaj>(pon);
            uto.Remove("08:00"); uto.Remove("09:00"); uto.Remove("20:00"); uto.Remove("22:00");
            Serija pce = DohvatiIzBazeSerije("Pcelica Maja"); uto.Add("08:00",pce);
            Serija tra = DohvatiIzBazeSerije("Traktor Tom"); uto.Add("09:00", tra);
            Film the = DohvatiIzBazeFilmove("The Help"); uto.Add("20:00", the);
            Film brz = DohvatiIzBazeFilmove("Brzi i zestoki"); uto.Add("22:00", brz);

            Dictionary<string, ITVSadrzaj> sri = new Dictionary<string, ITVSadrzaj>(pon);
            sri.Remove("20:00"); sri.Remove("22:00");
            Film sk5 = DohvatiIzBazeFilmove("Sam u Kuci 5"); sri.Add("20:00", sk5);
            Film ss = DohvatiIzBazeFilmove("Slagalica strave"); sri.Add("22:00", ss);

            Dictionary<string, ITVSadrzaj> cet = new Dictionary<string, ITVSadrzaj>(uto);
            cet.Remove("20:00"); cet.Remove("22:00");
            Film sk4 = DohvatiIzBazeFilmove("Sam u Kuci 4"); cet.Add("20:00", sk4);
            Film kru = DohvatiIzBazeFilmove("Krug"); cet.Add("22:00", kru);

            Dictionary<string, ITVSadrzaj> pet = new Dictionary<string, ITVSadrzaj>(pon);
            pet.Remove("20:00"); pet.Remove("22:00");
            Film sk3 = DohvatiIzBazeFilmove("Sam u Kuci 3"); pet.Add("20:00", sk3);
            Film tek = DohvatiIzBazeFilmove("Teksaski masakr motornom pilom"); pet.Add("22:00", tek);

            Dictionary<string, ITVSadrzaj> sub = new Dictionary<string, ITVSadrzaj>(pon);
            sub.Remove("08:00"); sub.Remove("09:00"); sub.Remove("10:00"); sub.Remove("11:00"); sub.Remove("20:00"); sub.Remove("22:00");
            Serija zek = DohvatiIzBazeSerije("Zekoslav Mrkva"); sub.Add("08:00", zek); sub.Add("09:00", zek); sub.Add("10:00", zek); sub.Add("11:00", zek);
            //Film the = DohvatiIzBazeFilmove("The Help"); sub.Add("20:00", the);
            //Film brz = DohvatiIzBazeFilmove("Brzi i zestoki"); sub.Add("22:00", brz);

            List<Dictionary<string, ITVSadrzaj>> list = new List<Dictionary<string, ITVSadrzaj>>();
            list.Add(pon);
            return list;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UnosPrograma();

            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            ListViewItem new_item = lvTvProgram.Items.Add("12:30");
            new_item.SubItems.Add("Regionalni Dnevnik");

            for (int i = 0; i < lvTvProgram.Columns.Count; i++)
                lvTvProgram.Columns[i].Width = -2;

            
            Film d = DohvatiIzBazeFilmove("Sam u Kuci");
            
            
                ListViewItem listitem = new ListViewItem(d.Ime);
                lvTvProgram.Items.Add(listitem);
                listitem.SubItems.Add(d.Duljina.ToString());
            
            txbOpis.Text = d.ToString();

            

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
                    MessageBox.Show(ex.Message, "Ooops" + f.Ime);
                }
                finally { connection.Close(); }

            }
        }

        private void UnosSerija(Serija f)
        {
            string query = "INSERT INTO Serija VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @redatelj, @sezona, @epizode, @prikazivanje)";
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
                command.Parameters.AddWithValue("@sezona", f.Sezona);
                command.Parameters.AddWithValue("@epizode", f.Epizode);
                command.Parameters.AddWithValue("@prikazivanje", f.Prikazivanje);


                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ooops" + f.Ime);
                }
                finally { connection.Close(); }

            }
        }

        private void UnosLivePrijenos(LivePrijenos f)
        {
            string query = "INSERT INTO LivePrijenos VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @tip, @prikazivanje)";
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
                command.Parameters.AddWithValue("@tip", f.Tip);
                command.Parameters.AddWithValue("@prikazivanje", f.Prikazivanje);


                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ooops" + f.Ime);
                }
                finally { connection.Close(); }

            }
        }

        private void UnosDSPK(DSPK f)
        {
            string query = "INSERT INTO DSPK VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @tip, @urednik, @voditelj, @prikazivanje)";
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
                command.Parameters.AddWithValue("@tip", f.Tip);
                command.Parameters.AddWithValue("@urednik", f.Urednik);
                command.Parameters.AddWithValue("@voditelj", f.Voditelj);
                command.Parameters.AddWithValue("@prikazivanje", f.Prikazivanje);


                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ooops0"+f.Ime);
                }
                finally { connection.Close(); }

            }
        }

        private void UnosReklama(Reklama f)
        {
            string query = "INSERT INTO Reklama VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @cijena)";
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
                command.Parameters.AddWithValue("@cijena", f.Cijena);


                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ooops" + f.Ime);
                }
                finally { connection.Close(); }

            }
        }

        private void UnosPrograma()
        {
            DSPK Dnevnik = new DSPK("Dnevnik", "Informativna emisija", "svijet", 60, 1, 8, "dnevnik", "Goran Milic", "Petar Pereza", "-XXXXXXX"); UnosDSPK(Dnevnik);
            DSPK Vijesti = new DSPK("Vijesti", "Informativna emisija", "Split", 60, 2, 8, "dnevnik", "Goran Milic", "Petar Pereza", "-XXXXXXX");UnosDSPK(Dnevnik);

            DSPK Prognoza = new DSPK("Prognoza", "Informativna emisija", "Hrvatska", 15, 2, 8, "vrijeme", "Goran Milic", "Petar Pereza", "-XXXXXXX");UnosDSPK(Prognoza);
            DSPK Sport = new DSPK("Sport", "Informativna emisija", "Svijet", 15, 2, 8, "sport", "Goran Milic", "Petar Pereza", "-XXXXXXX");UnosDSPK(Sport);

            DSPK InMagazin = new DSPK("InMagazin", "Lifestyle magazin", "Svijet", 30, 4, 8, "show", "Renata Končić", "Mia Kovačić", "-XXXXXXX");UnosDSPK(InMagazin);
            DSPK Potjera = new DSPK("Potjera", "Kviz", "Utrka pitanja", 90, 4, 8, "show", "Renata Končić", "Mia Kovačić", "-XXXXXXX");UnosDSPK(Potjera);
            DSPK DrOz = new DSPK("DrOz", "talk show", "Zdravlje.", 60, 4, 8, "talk show", "Dr Oz", "Dr Oz", "-XXXXX--");UnosDSPK(DrOz);
            DSPK DobroJutro = new DSPK("Dobro jutro Hrvatska", "jutarnja emisija", "aktualno", 120, 2, 0, "talk show", "Ana Milic", "Karmela Vukov", "-XXXXX--");UnosDSPK(DobroJutro);

            Film film1 = new Film("Bumbleblee", "Transformeri", "akcija", 90, 3, 0, "A.K.", "", "R-----XX");UnosFilm(film1);
            Film film2 = new Film("Sam u kući 1", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-----XX");UnosFilm(film2);
            Film film3 = new Film("Sam u kući 2", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-------"); UnosFilm(film3);
            Film film4 = new Film("Sam u kući 3", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-----XX"); UnosFilm(film4);
            Film film5 = new Film("Sam u kući 4", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-----XX"); UnosFilm(film5);
            Film film6 = new Film("Sam u kući 5", "Dječak u kući.", "obiteljski", 90, 3, 0, "K.E.", "P.O.", "R-------"); UnosFilm(film6);
            Film film7 = new Film("The help", "Istinit događaj.", "drama", 90, 3, 15, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film7);
            Film film8 = new Film("Brzi i žestoki", "Auti.", "akcija", 90, 3, 15, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film8);
            Film film9 = new Film("Slagalica strave", "Strah.", "horor", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film9);
            Film film10 = new Film("Krug", "Strah.", "horor", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film10);
            Film film11 = new Film("Teksaški masakr motornom pilom", "Strah.", "horor", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film11);
            Film film12 = new Film("SuperMan1", "Strah.", "akcija", 90, 3, 12, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film12);
            Film film13 = new Film("Harry Potter i kamen mudraca", "Čarobnjaci.", "sf", 90, 3, 18, "K.E.", "P.O.", "-XXXXX--"); UnosFilm(film13);


            Serija animirani1 = new Serija("Pokemoni", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "R-------");UnosSerija(animirani1);
            Serija animirani2 = new Serija("Teletabisi", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "-XXXXX--"); UnosSerija(animirani2);
            Serija animirani3 = new Serija("Pcelica Maja", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "R-------"); UnosSerija(animirani3);
            Serija animirani4 = new Serija("Traktor Tom", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "-XXXXX--"); UnosSerija(animirani4);
            Serija animirani5 = new Serija("Zekoslav Mrkva", "Zeko.", "animirani", 60, 3, 0, "R.E", "1",1, "-XXXXX--"); UnosSerija(animirani5);

            Serija serija1 = new Serija("Istanbulska nevjesta", "Turska", "drama", 60, 3, 12, "R.E", "1", 1, "-XXXXX--"); UnosSerija(serija1);
            Serija serija2 = new Serija("Kobra", "Njemacka autocesta", "akcija", 60, 3, 12, "R.E", "1", 1, "-XXXXX--"); UnosSerija(serija2);
            Serija serija3 = new Serija("Osveta ljubavi", "Turska", "romanticna drama", 60, 3, 12, "R.E", "1", 124, "-XXXXX--"); UnosSerija(serija3);
            Serija serija4 = new Serija("Dadilja", "Obitelj i dadilja", "obiteljska", 60, 3, 0, "R.E", "1", 65, "-XXXXX--"); UnosSerija(serija4);

            LivePrijenos liveprijenos1 = new LivePrijenos("The Voice", "glazbeni show", "natjecanje", 120, 3, 0, "live prijenos", "------XX"); UnosLivePrijenos(liveprijenos1);
            LivePrijenos liveprijenos2 = new LivePrijenos("Koncert", "live koncert", "glazba", 120, 2, 0, "live prijenos", "------X-"); UnosLivePrijenos(liveprijenos2);
            LivePrijenos liveprijenos3 = new LivePrijenos("Utakmica", "Hrvatska-Spanjolska", "sport", 120, 2, 0, "live prijenos", "-------X"); UnosLivePrijenos(liveprijenos3);

            Reklama reklama1 = new Reklama("Lenor", "omeksivac", "odjeca", 1, 4, 0, 45);UnosReklama(reklama1);
            Reklama reklama2 = new Reklama("Orbit", "zvaka", "prehrana", 2, 4, 0, 80); UnosReklama(reklama2);
            Reklama reklama3 = new Reklama("Karlovacko", "pivo", "prehrana", 3, 4, 0, 105); UnosReklama(reklama3);
            Reklama reklama4 = new Reklama("Ozujsko", "pivo", "prehrana", 1, 4, 0, 70); UnosReklama(reklama4);
            Reklama reklama5 = new Reklama("Lidl", "trgovina", "prehrana", 3, 4, 0, 130); UnosReklama(reklama5);
            Reklama reklama6 = new Reklama("Tommy", "trgovina", "prehrana", 2, 4, 0, 90); UnosReklama(reklama6);
            Reklama reklama7 = new Reklama("TopShop", "kupovina", "razno", 5, 4, 0, 200); UnosReklama(reklama7);
        }

        private void lvTvProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
