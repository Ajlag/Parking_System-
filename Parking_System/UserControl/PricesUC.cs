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
    public partial class PricesUC : UserControl
    {
        public PricesUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {         
                Base.Promenicene(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PricesUC_Load(object sender, EventArgs e)
        {
            DataTable dt = Base.selectCene();
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0][0].ToString();
                textBox2.Text = dt.Rows[1][0].ToString();
                textBox3.Text = dt.Rows[2][0].ToString();
                
            }
            else
            {
                MessageBox.Show("Check again");
            }
        }
    }
}
