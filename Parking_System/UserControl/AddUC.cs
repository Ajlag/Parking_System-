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
using FontAwesome.Sharp;

namespace Parking_System
{
    public partial class AddUC : UserControl
    {
        private List<mesta> listamesta = new List<mesta>();
        private readonly object btn;

        public AddUC()
        {
            InitializeComponent();
        }
        int k = 0;
        int count = 0;
        int orijentacija = 2;

        private void AddUC_MouseClick(object sender, MouseEventArgs e)
        {
            //na klik odredi trenutnu poziciju misa
            int x_pos = e.X;
            int y_pos = e.Y;
            int err_flag = 0;


            //Zabrani upis jednog preko drugog parking mesta -> prolazak kroz sva mesta i provera u odnosu na poziciju klika i izabranu orijentaciju
            SqlConnection con1 = new SqlConnection("data source=DESKTOP-L1JRQG0;initial catalog=parking_servis;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;");
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * From parkingmesto; ", con1);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
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

                    int orijentacija_baza = int.Parse(dt.Rows[i][6].ToString()); //orijentacija mesta u bazi podataka (0-horizontalno, 1-vertikalno)

                    //varijabla orijentacija je odredjena izborom horizontalnog (0) ili vertikalnog (1) izabranog parking mesta za upis

                    //Slucaj horizontalno hocemo da upisemo i uporedjujmo ga sa horizontalnim mestima iz baze
                    if (orijentacija == 0 && orijentacija_baza == 0 && (x_pos + 85 >= m.x && x_pos <= m.x + 85) && (y_pos + 50 >= m.y) && (y_pos <= m.y + 50))
                    {
                        err_flag = 1; //error flag -> nije moguce iscrtati jedno preko drugog
                        MessageBox.Show("It is not possible to add a parking space in this position.Choose another position.");
                    }


                    //Slucaj vertikalno hocemo da upisemo i uporedjujmo ga sa vertikalnim mestima iz baze 
                    if (orijentacija == 1 && orijentacija_baza == 1 && (x_pos + 53 >= m.x && x_pos <= m.x + 53) && (y_pos + 85 >= m.y) && (y_pos <= m.y + 85))
                    {
                        err_flag = 1;
                        MessageBox.Show("It is not possible to add a parking space in this position.Choose another position.");
                    }

                    //Slucaj vertikalno hocemo da upisemo i uporedjujmo ga sa horizontalnim mestima iz baze
                    if (orijentacija == 1 && orijentacija_baza == 0 && (x_pos + 53 >= m.x && x_pos <= m.x + 85) && (y_pos + 85 >= m.y) && (y_pos <= m.y + 50))
                    {
                        err_flag = 1;
                        MessageBox.Show("It is not possible to add a parking space in this position.Choose another position.");
                    }

                    //Slucaj horizontalno hocemo da upisemo i uporedjujmo ga sa vertikalnim mestima iz baze
                    if (orijentacija == 0 && orijentacija_baza == 1 && (x_pos + 85 >= m.x && x_pos <= m.x + 53) && (y_pos + 50 >= m.y) && (y_pos <= m.y + 85))
                    {
                        err_flag = 1;
                        MessageBox.Show("It is not possible to add a parking space in this position.Choose another position.");
                    }
                    i++;
                }
            }
            //Zabrani upis jednog preko drugog parking mesta -> err_flag == 0?
            //Da li si odabrao orijentaciju parking mesta (inicijalno je 2)? --> ako nisi nikom nista, dzabe klikces
            if (textBox1.Text.Length != 0 && orijentacija != 2 && err_flag == 0)  //Da li si upisao oznaku parking mesta? --> ako nisi nikom nista, dzabe klikces
            {
                SqlConnection com = new SqlConnection("data source=DESKTOP-L1JRQG0;initial catalog=parking_servis;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;");
                try
                {
                    //insertuj novo parking mesto na poziciji x_pos i y_pos, id uzima vrednost brojaca count
                    com.Open();
                    SqlCommand sde = new SqlCommand("Insert into parkingmesto (oznaka, trenutnoStanje, id, x, y, vremeDolaska, Orijentacija ) values( '" + textBox1.Text + "', '0' ,'" + count + "','" + x_pos + "', '" + y_pos + "', '0', '" + orijentacija + "'); ", com);
                    sde.ExecuteNonQuery();
                    MessageBox.Show("Successful");
                    com.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //funkcija za iscrtavanje tek dodatog dugmeta
                mesta m = new mesta();
                m.oznaka = textBox1.Text;
                m.x = int.Parse(x_pos.ToString());
                m.y = int.Parse(y_pos.ToString());

                Button btn = new Button();
                if (orijentacija == 0) //Zelimo horizontalno da iscrtavamo mesta u ovoj oblasti
                {
                    btn.Location = new Point(m.x, m.y);
                    btn.Name = m.oznaka;
                    btn.Text = m.oznaka;
                    btn.Size = new Size(80, 45);
                    btn.BringToFront();
                    btn.BackColor = Color.Green;
                }
                else //U ostalim oblastima iscrtavaj vertikalno
                {
                    btn.Location = new Point(m.x, m.y);
                    btn.Name = m.oznaka;
                    btn.Text = m.oznaka;
                    btn.Size = new Size(48, 80);
                    btn.BringToFront();
                    btn.BackColor = Color.Green;
                }

                this.Controls.Add(btn);
                listamesta.Add(m);
            }
            count++;
        }

        private void AddUC_Load(object sender, EventArgs e)
        {
            //samo prvo ispisivanje mesta iz baze (brojac k obezbedjuje samo jedan ulazak u ovaj deo koda)
            if (k == 0)
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

                        IconButton btn = new IconButton();
                        //iscrtavanje standardnih mesta A1-A5, A10-A24
                        if (orijentacija == 0)
                        {
                            btn.Location = new Point(m.x, m.y);
                            btn.Name = m.oznaka;
                          //  btn.Text = m.oznaka;
                            btn.Size = new Size(80, 45);
                            btn.BringToFront();
                        }
                        else
                        {
                            btn.Location = new Point(m.x, m.y);
                            btn.Name = m.oznaka;
                            //btn.Text = m.oznaka;
                            btn.Size = new Size(48, 80);
                            btn.BringToFront();
                            btn.BackColor = Color.Green;

                        }
                        if (m.trenutnoStanje == 0)
                        {
                            btn.BackColor = Color.Green;                          
                        }
                        else
                        {
                            btn.IconChar = IconChar.CarSide;
                            btn.IconColor = Color.Red;
                            btn.IconSize =  45;
                           // btn.BackColor = Color.Red;
                        }

                        this.Controls.Add(btn);
                        listamesta.Add(m);
                        i++;
                        //brojac mesta u bazi
                        count++;
                    }
                    k = 1;          //ispisana su sva mesta zatecena u bazi --> ne ulazi vise ovde
                    count++;        //prebrojana su sva mesta prvobitno zatecena u bazi, postavi brojac id na vrednost sledeceg
                }
                else
                {
                    MessageBox.Show("Error loading parking space");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.BackColor = Color.Pink;
            button2.BackColor = Color.Gainsboro;
            orijentacija = 0;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.BackColor = Color.Pink;
            button1.BackColor = Color.Gainsboro;
            orijentacija = 1;
        }
    }
}
