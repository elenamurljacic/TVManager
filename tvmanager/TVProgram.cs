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
        

        private void Form1_Load(object sender, EventArgs e)
        {
            UnosPrograma();
        }

        public TVProgram()
        {
            InitializeComponent();

            monitor.Show();
      
            connectionString = ConfigurationManager.ConnectionStrings["tvmanager.Properties.Settings.TVSadrzajConnectionString"].ConnectionString;
            
           
        }

        private void Raspored(string dan)
        {
            Dictionary<string, ITVSadrzaj> pon = new Dictionary<string, ITVSadrzaj>();
            LivePrijenos vatrica = DohvatiIzBazeLivePrijenose("vatrica"); pon.Add("00:00", vatrica);
            DSPK dbh = DohvatiIzBazeDSPK("Dobro jutro hrvatska"); pon.Add("06:00", dbh);
            Serija tel = DohvatiIzBazeSerije("Teletabisi"); pon.Add("08:00", tel);
            Serija pok = DohvatiIzBazeSerije("Digimoni"); pon.Add("09:00", pok);
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
            DSPK inm = DohvatiIzBazeDSPK("InMagazin"); pon.Add("19:30", inm);
            Film sup = DohvatiIzBazeFilmove("SuperMan1"); pon.Add("20:00", sup);
            Film hp = DohvatiIzBazeFilmove("Harry Potter i kamen mudraca"); pon.Add("22:00", hp);

            Dictionary<string, ITVSadrzaj> uto = new Dictionary<string, ITVSadrzaj>(pon);
            uto.Remove("08:00"); uto.Remove("09:00"); uto.Remove("20:00"); uto.Remove("22:00");
            Serija pce = DohvatiIzBazeSerije("Bob graditelj"); uto.Add("08:00",pce);
            Serija tra = DohvatiIzBazeSerije("Traktor Tom"); uto.Add("09:00", tra);
            Film the = DohvatiIzBazeFilmove("The Help"); uto.Add("20:00", the);
            Film brz = DohvatiIzBazeFilmove("Brzi i žestoki"); uto.Add("22:00", brz);

            Dictionary<string, ITVSadrzaj> sri = new Dictionary<string, ITVSadrzaj>(pon);
            sri.Remove("20:00"); sri.Remove("22:00");
            Film ig1 = DohvatiIzBazeFilmove("Igre gladi 1"); sri.Add("20:00", ig1);
            Film ss = DohvatiIzBazeFilmove("Slagalica strave"); sri.Add("22:00", ss);

            Dictionary<string, ITVSadrzaj> cet = new Dictionary<string, ITVSadrzaj>(uto);
            cet.Remove("20:00"); cet.Remove("22:00");
            Film ig2 = DohvatiIzBazeFilmove("Igre gladi 2"); cet.Add("20:00", ig2);
            Film kru = DohvatiIzBazeFilmove("Krug"); cet.Add("22:00", kru);

            Dictionary<string, ITVSadrzaj> pet = new Dictionary<string, ITVSadrzaj>(pon);
            pet.Remove("20:00"); pet.Remove("22:00");
            Film ig3 = DohvatiIzBazeFilmove("Igre gladi 3"); pet.Add("20:00", ig3);
            Film tek = DohvatiIzBazeFilmove("Teksaški masakr motornom pilom"); pet.Add("22:00", tek);

            Dictionary<string, ITVSadrzaj> sub = new Dictionary<string, ITVSadrzaj>(pon);
            sub.Remove("08:00"); sub.Remove("09:00"); sub.Remove("10:00"); sub.Remove("11:00"); sub.Remove("13:15"); sub.Remove("14:15"); sub.Remove("15:00"); sub.Remove("16:30"); sub.Remove("20:00"); sub.Remove("22:00");
            Serija zek = DohvatiIzBazeSerije("Zekoslav Mrkva"); sub.Add("08:00", zek); sub.Add("09:00", zek); sub.Add("10:00", zek); sub.Add("11:00", zek);
            LivePrijenos uta = DohvatiIzBazeLivePrijenose("Utakmica"); sub.Add("13:15", uta);
            LivePrijenos kon = DohvatiIzBazeLivePrijenose("Koncert"); sub.Add("15:30", kon);
            Film sk1 = DohvatiIzBazeFilmove("Sam u kući 1"); sub.Add("20:00", sk1);
            Film bum = DohvatiIzBazeFilmove("Bumbleblee"); sub.Add("22:00", bum);

            Dictionary<string, ITVSadrzaj> ned = new Dictionary<string, ITVSadrzaj>(sub);
            ned.Remove("20:00"); ned.Remove("22:00");
            LivePrijenos voi = DohvatiIzBazeLivePrijenose("The Voice"); ned.Add("20:00", voi);
            Film sk3 = DohvatiIzBazeFilmove("Sam u kući 3"); ned.Add("22:00", sk3);

            List<Dictionary<string, ITVSadrzaj>> list = new List<Dictionary<string, ITVSadrzaj>>();
            list.Add(pon); list.Add(uto); list.Add(sri); list.Add(cet); list.Add(pet); list.Add(sub); list.Add(ned);


            if (dan == "Pon")
            {
                BrisiListView();

                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(tel.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(pok.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(osv.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(ist.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(cob.Ime);
                ListViewItem listitem10 = new ListViewItem("14:15");
                lvTvProgram.Items.Add(listitem10);
                listitem10.SubItems.Add(dad.Ime);
                ListViewItem listitem11 = new ListViewItem("15:00");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(droz.Ime);
                //reklama
                ListViewItem listitem12 = new ListViewItem("16:30");
                lvTvProgram.Items.Add(listitem12);
                listitem12.SubItems.Add(pot.Ime);
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(sup.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(hp.Ime);

            }

            if(dan == "Uto")
            {
                BrisiListView();
                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(pce.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(tra.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(osv.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(ist.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(cob.Ime);
                ListViewItem listitem10 = new ListViewItem("14:15");
                lvTvProgram.Items.Add(listitem10);
                listitem10.SubItems.Add(dad.Ime);
                ListViewItem listitem11 = new ListViewItem("15:00");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(droz.Ime);
                //reklama
                ListViewItem listitem12 = new ListViewItem("16:30");
                lvTvProgram.Items.Add(listitem12);
                listitem12.SubItems.Add(pot.Ime);
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(the.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(brz.Ime);
            }

            if(dan == "Sri")
            {
                BrisiListView();
                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(tel.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(pok.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(osv.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(ist.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(cob.Ime);
                ListViewItem listitem10 = new ListViewItem("14:15");
                lvTvProgram.Items.Add(listitem10);
                listitem10.SubItems.Add(dad.Ime);
                ListViewItem listitem11 = new ListViewItem("15:00");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(droz.Ime);
                //reklama
                ListViewItem listitem12 = new ListViewItem("16:30");
                lvTvProgram.Items.Add(listitem12);
                listitem12.SubItems.Add(pot.Ime);
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(ig1.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(brz.Ime);
            }

            if(dan == "Cet")
            {
                BrisiListView();
                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(pce.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(tra.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(osv.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(ist.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(cob.Ime);
                ListViewItem listitem10 = new ListViewItem("14:15");
                lvTvProgram.Items.Add(listitem10);
                listitem10.SubItems.Add(dad.Ime);
                ListViewItem listitem11 = new ListViewItem("15:00");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(droz.Ime);
                //reklama
                ListViewItem listitem12 = new ListViewItem("16:30");
                lvTvProgram.Items.Add(listitem12);
                listitem12.SubItems.Add(pot.Ime);
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(ig2.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(kru.Ime);
            }

            if(dan == "Pet")
            {
                BrisiListView();
                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(tel.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(pok.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(osv.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(ist.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(cob.Ime);
                ListViewItem listitem10 = new ListViewItem("14:15");
                lvTvProgram.Items.Add(listitem10);
                listitem10.SubItems.Add(dad.Ime);
                ListViewItem listitem11 = new ListViewItem("15:00");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(droz.Ime);
                //reklama
                ListViewItem listitem12 = new ListViewItem("16:30");
                lvTvProgram.Items.Add(listitem12);
                listitem12.SubItems.Add(pot.Ime);
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(ig3.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(tek.Ime);
            }

            if(dan == "Sub")
            {
                BrisiListView();
                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(zek.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(zek.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(zek.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(zek.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(uta.Ime);
                ListViewItem listitem11 = new ListViewItem("15:30");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(kon.Ime);
                //reklama
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(sk1.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(bum.Ime);
            }

            if(dan == "Ned")
            {
                BrisiListView();
                ListViewItem listitem1 = new ListViewItem("00:00");
                lvTvProgram.Items.Add(listitem1);
                listitem1.SubItems.Add(vatrica.Ime);
                ListViewItem listitem2 = new ListViewItem("06:00");
                lvTvProgram.Items.Add(listitem2);
                listitem2.SubItems.Add(dbh.Ime);
                ListViewItem listitem3 = new ListViewItem("08:00");
                lvTvProgram.Items.Add(listitem3);
                listitem3.SubItems.Add(zek.Ime);
                ListViewItem listitem4 = new ListViewItem("09:00");
                lvTvProgram.Items.Add(listitem4);
                listitem4.SubItems.Add(zek.Ime);
                ListViewItem listitem5 = new ListViewItem("10:00");
                lvTvProgram.Items.Add(listitem5);
                listitem5.SubItems.Add(zek.Ime);
                ListViewItem listitem6 = new ListViewItem("11:00");
                lvTvProgram.Items.Add(listitem6);
                listitem6.SubItems.Add(zek.Ime);
                ListViewItem listitem7 = new ListViewItem("12:00");
                lvTvProgram.Items.Add(listitem7);
                listitem7.SubItems.Add(vij.Ime);
                ListViewItem listitem8 = new ListViewItem("13:00");
                lvTvProgram.Items.Add(listitem8);
                listitem8.SubItems.Add(pro.Ime);
                ListViewItem listitem9 = new ListViewItem("13:15");
                lvTvProgram.Items.Add(listitem9);
                listitem9.SubItems.Add(uta.Ime);
                ListViewItem listitem11 = new ListViewItem("15:30");
                lvTvProgram.Items.Add(listitem11);
                listitem11.SubItems.Add(kon.Ime);
                //reklama
                ListViewItem listitem13 = new ListViewItem("18:00");
                lvTvProgram.Items.Add(listitem13);
                listitem13.SubItems.Add(dne.Ime);
                ListViewItem listitem14 = new ListViewItem("19:00");
                lvTvProgram.Items.Add(listitem14);
                listitem14.SubItems.Add(pro.Ime);
                ListViewItem listitem15 = new ListViewItem("19:15");
                lvTvProgram.Items.Add(listitem15);
                listitem15.SubItems.Add(spo.Ime);
                ListViewItem listitem16 = new ListViewItem("19:30");
                lvTvProgram.Items.Add(listitem16);
                listitem16.SubItems.Add(inm.Ime);
                ListViewItem listitem17 = new ListViewItem("20:00");
                lvTvProgram.Items.Add(listitem17);
                listitem17.SubItems.Add(voi.Ime);
                ListViewItem listitem18 = new ListViewItem("22:00");
                lvTvProgram.Items.Add(listitem18);
                listitem18.SubItems.Add(sk3.Ime);
            }

        }

        private void BrisiListView()
        {
            lvTvProgram.Clear();
            lvTvProgram.Columns.Add("Sat", -2, HorizontalAlignment.Left);
            lvTvProgram.Columns.Add("Naslov", -2, HorizontalAlignment.Left);

            for (int i = 0; i < lvTvProgram.Columns.Count; i++)
                lvTvProgram.Columns[i].Width = -2;
        }

        #region Odabir dana
        private void tsslPon_Click(object sender, EventArgs e)
        {
            Raspored("Pon");
        }

        private void tsslUto_Click(object sender, EventArgs e)
        {
            Raspored("Uto");
        }

        private void tsslSri_Click(object sender, EventArgs e)
        {
            Raspored("Sri");
        }

        private void tsslCet_Click(object sender, EventArgs e)
        {
            Raspored("Cet");
        }

        private void tsslPet_Click(object sender, EventArgs e)
        {
            Raspored("Pet");
        }

        private void tsslSub_Click(object sender, EventArgs e)
        {
            Raspored("Sub");
        }

        private void tsslNed_Click(object sender, EventArgs e)
        {
            Raspored("Ned");
        }

        #endregion


        #region Unos u bazu
        private void UnosFilm(Film f)
        {
            string query = "IF NOT EXISTS (Select Ime From Film where Ime = @Ime) INSERT INTO Film VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @redatelj, @gglumac, @prikazivanje) ";
               
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
            string query = "IF NOT EXISTS (Select Ime From Serija where Ime = @Ime) insert into Serija(Ime,Opis,Zanr,Duljina,Prioritet,DobnaSkupina,Redatelj,Sezona,Epizode,Prikazivanje) values(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @redatelj, @sezona, @epizode, @prikazivanje)";
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
            string query = "IF NOT EXISTS (Select Ime From LivePrijenos where Ime = @Ime) INSERT INTO LivePrijenos VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @tip, @prikazivanje)";
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
            string query = "IF NOT EXISTS (Select Ime From DSPK where Ime = @Ime) INSERT INTO DSPK VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @tip, @urednik, @voditelj, @prikazivanje)";
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
            string query = "IF NOT EXISTS (Select Ime From Reklama where Ime = @Ime) INSERT INTO Reklama VALUES(@ime, @opis, @zanr, @duljina, @prioritet, @dobnaskupina, @cijena)";
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
            DSPK Vijesti = new DSPK("Vijesti", "Informativna emisija", "Split", 60, 2, 8, "dnevnik", "Goran Milic", "Petar Pereza", "-XXXXXXX");UnosDSPK(Vijesti);

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
            Film film14 = new Film("Igre gladi 1", "Borba za opstanak.", "sf", 90, 3, 15, "K.E.", "P.O.", "-XXXXXX-"); UnosFilm(film14);
            Film film15 = new Film("Igre gladi 2", "Borba za opstanak.", "sf", 90, 3, 15, "K.E.", "P.O.", "-XXXXXX-"); UnosFilm(film15);
            Film film16 = new Film("Igre gladi 3", "Borba za opstanak.", "sf", 90, 3, 15, "K.E.", "P.O.", "-XXXXXX-"); UnosFilm(film16);
            Film film17 = new Film("Igre gladi 4", "Borba za opstanak.", "sf", 90, 3, 15, "K.E.", "P.O.", "-XXXXXX-"); UnosFilm(film17);


            Serija animirani1 = new Serija("Pokemoni", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "R-------");UnosSerija(animirani1);
            Serija animirani2 = new Serija("Teletabisi", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "-XXXXX--"); UnosSerija(animirani2);
            Serija animirani3 = new Serija("Pcelica Maja", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "R-------"); UnosSerija(animirani3);
            Serija animirani4 = new Serija("Traktor Tom", "Djeca.", "animirani", 60, 3, 0, "R.E", "1",1, "-XXXXX--"); UnosSerija(animirani4);
            Serija animirani5 = new Serija("Zekoslav Mrkva", "Zeko.", "animirani", 60, 3, 0, "R.E", "1",1, "-XXXXX--"); UnosSerija(animirani5);
            Serija animirani6 = new Serija("Digimoni", "Djeca.", "animirani", 60, 3, 0, "R.E", "1", 1, "R-------"); UnosSerija(animirani6);
            Serija animirani7 = new Serija("Bob graditelj", "Djeca.", "animirani", 60, 3, 0, "R.E", "1", 1, "R-------"); UnosSerija(animirani7);

            Serija serija1 = new Serija("Istanbulska nevjesta", "Turska", "drama", 60, 3, 12, "R.E", "1", 1, "-XXXXX--"); UnosSerija(serija1);
            Serija serija2 = new Serija("Kobra", "Njemacka autocesta", "akcija", 60, 3, 12, "R.E", "1", 1, "-XXXXX--"); UnosSerija(serija2);
            Serija serija3 = new Serija("Osveta ljubavi", "Turska", "romanticna drama", 60, 3, 12, "R.E", "1", 124, "-XXXXX--"); UnosSerija(serija3);
            Serija serija4 = new Serija("Dadilja", "Obitelj i dadilja", "obiteljska", 60, 3, 0, "R.E", "1", 65, "-XXXXX--"); UnosSerija(serija4);

            LivePrijenos liveprijenos1 = new LivePrijenos("The Voice", "glazbeni show", "natjecanje", 120, 3, 0, "live prijenos", "------XX"); UnosLivePrijenos(liveprijenos1);
            LivePrijenos liveprijenos2 = new LivePrijenos("Koncert", "live koncert", "glazba", 120, 2, 0, "live prijenos", "------X-"); UnosLivePrijenos(liveprijenos2);
            LivePrijenos liveprijenos3 = new LivePrijenos("Utakmica", "Hrvatska-Spanjolska", "sport", 120, 2, 0, "live prijenos", "-------X"); UnosLivePrijenos(liveprijenos3);
            LivePrijenos liveprijenos4 = new LivePrijenos("vatrica", "Prekid programa", "", 360, 2, 0, "live prijenos", "-------X"); UnosLivePrijenos(liveprijenos4);
            Reklama reklama1 = new Reklama("Lenor", "omeksivac", "odjeca", 1, 4, 0, 45);UnosReklama(reklama1);
            Reklama reklama2 = new Reklama("Orbit", "zvaka", "prehrana", 2, 4, 0, 80); UnosReklama(reklama2);
            Reklama reklama3 = new Reklama("Karlovacko", "pivo", "prehrana", 3, 4, 0, 105); UnosReklama(reklama3);
            Reklama reklama4 = new Reklama("Ozujsko", "pivo", "prehrana", 1, 4, 0, 70); UnosReklama(reklama4);
            Reklama reklama5 = new Reklama("Lidl", "trgovina", "prehrana", 3, 4, 0, 130); UnosReklama(reklama5);
            Reklama reklama6 = new Reklama("Tommy", "trgovina", "prehrana", 2, 4, 0, 90); UnosReklama(reklama6);
            Reklama reklama7 = new Reklama("TopShop", "kupovina", "razno", 5, 4, 0, 200); UnosReklama(reklama7);
        }
        #endregion


        #region Dohvacanje iz baze
        private Film DohvatiIzBazeFilmove(string ime)
        {
            Film flm = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Film WHERE Ime LIKE '" + ime + "'", connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Serija WHERE Ime LIKE '" + ime + "'", connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ser = new Serija(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Redatelj"].ToString(), dr["Sezona"].ToString(), (int)dr["Epizode"], dr["Prikazivanje"].ToString());
                }
            }
            return ser;
        }
        private LivePrijenos DohvatiIzBazeLivePrijenose(string ime)
        {
            LivePrijenos liv = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM LivePrijenos WHERE Ime LIKE '" + ime + "'", connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    liv = new LivePrijenos(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], dr["Tip"].ToString(), dr["Prikazivanje"].ToString());
                }
            }
            return liv;
        }

        private DSPK DohvatiIzBazeDSPK(string ime)
        {
            DSPK dsp = null;
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM DSPK WHERE Ime LIKE '" + ime + "'", connection)) //treba mi LIKE
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
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Reklama", connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    rek.Add(new Reklama(dr["Ime"].ToString(), dr["Opis"].ToString(), dr["Zanr"].ToString(), (int)dr["Duljina"], (int)dr["Prioritet"], (int)dr["DobnaSkupina"], (int)dr["Cijena"]));
                }
            }
            return rek;
        }
        #endregion

        private void lvTvProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnIzvanrednaSituacija_Click(object sender, EventArgs e)
        {
            if (lvTvProgram.Items.Count < 1)
            {
                MessageBox.Show("Odaberite dan za izvanrednu situaciju");
            }
            else
            {
                IzvanRednaSituacija izv = new IzvanRednaSituacija();
                izv.Show();
                string ime = izv.textBox1.Text;
                string duljinaTrajanja = izv.textBox2.Text;
                List<string[]> novaLista = new List<string[]>();

                //Prioritet ne smijemo dirat

                //i brojimo reklame

                
                for (int i=0;i< lvTvProgram.Items.Count; i++)
                {
                    string[] program = new string[3];
                    try
                    {
                        DSPK d = DohvatiIzBazeDSPK(lvTvProgram.Items[i].SubItems[1].Text);
                        program[0] = d.Ime;
                        program[1] = "" + d.Duljina;
                        program[2] = "" + d.Prioritet;
                    }
                    catch{ }
                    try
                    {
                        Film d = DohvatiIzBazeFilmove(lvTvProgram.Items[i].SubItems[1].Text);
                        program[0] = d.Ime;
                        program[1] = "" + d.Duljina;
                        program[2] = "" + d.Prioritet;
                    }
                    catch { }
                    try
                    {
                        Serija d = DohvatiIzBazeSerije(lvTvProgram.Items[i].SubItems[1].Text);
                        program[0] = d.Ime;
                        program[1] = "" + d.Duljina;
                        program[2] = "" + d.Prioritet;
                    
                    }
                    catch { }
                    try
                    {
                        //Reklama d = DohvatiIzBazeReklame(lvTvProgram.Items[i].SubItems[1].Text);
                    }
                }

                
            }

        }
    }
}
