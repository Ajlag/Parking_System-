using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking_System
{
    public partial class EmployeeUC : UserControl
    {
        public EmployeeUC()
        {
            InitializeComponent();
            popuniTabelu();
            this.Dock = DockStyle.Fill; popuniTabelu();
            this.Dock = DockStyle.Fill;
        }
 
        private void popuniTabelu()
        {
            var termini = Base.popuniTabelutermin();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = termini;
        }
        private void EmployeeUC_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            korisnik k = new korisnik();
            k.korisnickoIme = textBox5.Text;
            string allowedchar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            k.sifra = textBox4.Text;

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("You must fill in all the fields!");
            }
            else if (k.proveraKorisnika())
            {
                MessageBox.Show("Username already exists.");
            }
            else if (!textBox5.Text.All(allowedchar.Contains))
            {
                MessageBox.Show("Check the password.You have entered forbidden characters");
            }
            else if (!textBox4.Text.All(allowedchar.Contains))
            {
                MessageBox.Show("Check the username.You have entered forbidden characters!");
            }
            else
            {                
                try
                {
                    Base.dodavanjeRadnika(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                    MessageBox.Show("Successful");
                }
                catch
                {
                    MessageBox.Show("Employee already exists!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult Izlaz;
            Izlaz = MessageBox.Show("Do you want to delete selected employee?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Izlaz == DialogResult.Yes)
            {
                try
                {
                    Base.brisanjeRadnika(textBox5.Text, textBox1.Text, textBox2.Text);
                    MessageBox.Show("Successful");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";                
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            korisnik k = new korisnik();
            DataTable dt = Base.radniciBox(k);
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0][0].ToString();
                textBox2.Text = dt.Rows[0][1].ToString();
                textBox3.Text = dt.Rows[0][2].ToString();
                textBox5.Text = dt.Rows[0][4].ToString();
            }
            else
            {
                MessageBox.Show("Check username");
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string name = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                string surname = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
                string mobile_number = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;
              //  string password = dataGridView1.SelectedRows[0].Cells[3].Value + string.Empty;
                string username = dataGridView1.SelectedRows[0].Cells[4].Value + string.Empty;

                textBox1.Text = name;
                textBox2.Text = surname;
                textBox3.Text = mobile_number;
                textBox5.Text = username;
               // textBox4.Text = password;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 3 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
