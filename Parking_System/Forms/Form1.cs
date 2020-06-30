using Parking_System.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            korisnik k = new korisnik();
            k.korisnickoIme = textBox1.Text;
            k.sifra = textBox2.Text;
            DataTable dt1 = Base.UlogujAdmina(k);
            DataTable dt = Base.UlogujRadnika(k);
            string allowedchar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Morate popuniti sva polja");
                }
                else if (!textBox2.Text.All(allowedchar.Contains))
                {
                    MessageBox.Show("Proverite lozinku");
                }
                else
                {
                    if (k.proveraKorisnika())
                    {
                        if (k.tip == "A")
                        {
                            if (dt1.Rows.Count == 1)

                            {
                                this.Hide();
                                Admin a = new Admin();
                                a.Show();
                            }
                            else
                            {
                                MessageBox.Show("Neispravno ste uneli lozinku.");
                            }
                        }
                        else
                        {
                            if (dt.Rows.Count == 1)
                            {
                                this.Hide();
                                User f = new User();
                                f.Show();
                            }
                            else
                            {
                                MessageBox.Show("Neispravno ste uneli lozinku.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ne postoji korisnik sa unesenim korisničkim imenom");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            a.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           // textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            takeover t = new takeover();
            t.Show();
        }
    }
}
