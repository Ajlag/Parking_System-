using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_System
{
    class Base
    {
        private static string connectionString = @"data source =DESKTOP-L1JRQG0; initial catalog = parking_servis; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework & quot;";
        private static SqlConnection connect = new SqlConnection(connectionString);

        public static bool ProveraKorisnika(korisnik k)
        {
            string sql = "SELECT korisnickoIme FROM korisnik";
            SqlCommand cmd = new SqlCommand(sql, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            connect.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == k.korisnickoIme)
                {
                    connect.Close();
                    return true;
                }
            }
            connect.Close();
            return false;
        }
        public static DataTable UlogujAdmina(korisnik k)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM korisnik WHERE korisnickoIme = @korisnickoIme AND sifra = @sifra and tip='A'";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@korisnickoIme", k.korisnickoIme);
            cmd.Parameters.AddWithValue("@sifra", k.sifra);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            connect.Open();
            adapter.Fill(dt);
            connect.Close();

            return dt;
        }
        public static DataTable UlogujRadnika(korisnik k)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM korisnik WHERE korisnickoIme = @korisnickoIme AND sifra = @sifra and tip='R'";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@korisnickoIme", k.korisnickoIme);
            cmd.Parameters.AddWithValue("@sifra", k.sifra);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            connect.Open();
            adapter.Fill(dt);
            connect.Close();

            return dt;
        }

        public static void Promenicene(int cena1, int cena2, int cena3)
        {

            connect.Open();
            SqlCommand sda = new SqlCommand("Update cene set cena =" + cena1 + " where tip = 'S';Update cene set cena =" + cena2 + " where tip= 'D';Update cene set cena =" + cena3 + " where tip= 'M'; ", connect);

            sda.ExecuteNonQuery();
            connect.Close();


        }
        public static DataTable selectCene()
        {
            connect.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * From cene ORDER BY cena;", connect);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            connect.Close();
            return dt;
        }

        public static void dodavanjeRadnika(string tbIme, string tbPrezime, string tbbrTel, string tbSifra, string tbKorisnickoIme)
        {
            connect.Open();
            SqlCommand sda = new SqlCommand("Insert into korisnik (ime, prezime, brTelefona, sifra, korisnickoIme, tip) values('" + tbIme + "','" + tbPrezime + "','" + tbbrTel + "','" + tbSifra + "', '" + tbKorisnickoIme + "', 'R');", connect);
            sda.ExecuteNonQuery();
            connect.Close();
        }
        public static void brisanjeRadnika(string tbKorisnickoIme, string tbIme, string tbPrezime)
        {
            connect.Open();
            SqlCommand sda = new SqlCommand("DELETE FROM korisnik where korisnickoIme ='" + tbKorisnickoIme + "';Delete from  korisnik where Ime='" + tbIme + "' and Prezime='" + tbPrezime + "' ", connect);
            sda.ExecuteNonQuery();
            connect.Close();
        }
        public static DataTable radniciBox(korisnik k)
        {
            connect.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ime, prezime, brtelefona, korisnickoIme FROM korisnik WHERE tip='R'", connect);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            connect.Close();
            return dt;
        }

        public static List<korisnik> popuniTabelutermin()
        {
            parking_servisEntities1 vezasaBazom = new parking_servisEntities1();
            List<korisnik> korisnik = vezasaBazom.korisniks.Where(t => t.tip == "R").ToList();
            return korisnik;
        }
    }
}

