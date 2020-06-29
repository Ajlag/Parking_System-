using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Parking_System
{
    public partial class ShowUC : UserControl
    { 
     private List<mesta> listamesta = new List<mesta>();
        public ShowUC()
        {
            InitializeComponent();
        }

        private void ShowUC_Load(object sender, EventArgs e)
        {                     
            SqlConnection con = new SqlConnection("data source=DESKTOP-L1JRQG0;initial catalog=parking_servis;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;");
            SqlDataAdapter sda = new SqlDataAdapter("Select * From parkingmesto; ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                while (i < dt.Rows.Count)
                {

                    mesta m = new mesta();
                    m.oznaka = dt.Rows[i][0].ToString();
                    m.trenutnoStanje = int.Parse(dt.Rows[i][1].ToString());
                    m.id = int.Parse(dt.Rows[i][2].ToString());
                    m.x = int.Parse(dt.Rows[i][3].ToString());
                    m.y = int.Parse(dt.Rows[i][4].ToString());

                    int orijentacija = int.Parse(dt.Rows[i][6].ToString());

                    int x_pos = m.x;
                    int y_pos = m.y;

                    Button btn = new Button();

                    if (orijentacija == 0)    //orijentacija 0 - za iscrtavanje horizontalno postavljenih mesta
                    {
                        btn.Location = new Point(m.x, m.y);
                        btn.Name = m.oznaka;
                        btn.Text = m.oznaka;
                        btn.Size = new Size(80, 45);
                        btn.BringToFront();
                       // btn.Click += new EventHandler(this.button_click);
                    }
                    else                      //orijentacija 1 - za iscrtavanje vertikalno postavljenih mesta
                    {
                        btn.Location = new Point(m.x, m.y);
                        btn.Name = m.oznaka;
                        btn.Text = m.oznaka;
                        btn.Size = new Size(48, 80);
                        btn.BringToFront();
                       // btn.Click += new EventHandler(this.button_click);
                    }
                    if (m.trenutnoStanje == 0)
                    {
                        btn.BackColor = Color.Green;
                    }
                    else
                    {
                        btn.BackColor = Color.Red;
                    }

                    this.Controls.Add(btn);
                    listamesta.Add(m);
                    i++;
                }
            }
            else
            {
                MessageBox.Show("Greska kod ucitavanja parking mesta");
            }
        }
    }
}

