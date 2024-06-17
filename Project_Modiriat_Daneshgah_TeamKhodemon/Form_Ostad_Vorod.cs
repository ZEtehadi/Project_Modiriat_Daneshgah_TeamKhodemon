using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_Modiriat_Daneshgah_TeamKhodemon
{
    public partial class Form_Ostad_Vorod : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        Form_Ostad_EdaeeDars F_EraeeDars = new Form_Ostad_EdaeeDars();
        Form_Osatad_BarnameHaftegi F_BarnameHaftagi = new Form_Osatad_BarnameHaftegi();
        public Form_Ostad_Vorod()
        {
            InitializeComponent();
        }

        private void Form_Ostad_Vorood_Load(object sender, EventArgs e)
        {
            this.Left = 210;
            this.Top = 95;

            txtOstad_FName.Text = Class1.TeachName.ToString();
            txtOstad_LName.Text = Class1.TeachLastName.ToString();
            txtOstad_FatherName.Text = Class1.TeachFather.ToString();
            txtOstad_Code.Text = Class1.TeachCode.ToString();
            txtOstad_Madrak.Text = Class1.TeachMadrak.ToString();
            txtOstad_NationalCode.Text = Class1.TeachNationalCode.ToString();
            txtOstad_Phone.Text = Class1.TeachPhone.ToString();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا از خروج اطمینان دارید ؟؟",
                                              "اطلاعیه !!!",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                this.Close();
                new Form1().Show();
            }
        }

        private void btnEreeDars_Click(object sender, EventArgs e)
        {
            F_EraeeDars.Visible = true;
            if (F_BarnameHaftagi.Visible == true)
                F_BarnameHaftagi.Visible = false;
    
        }


        private void btnBarnameHeftegi_Click_1(object sender, EventArgs e)
        {
            F_BarnameHaftagi.Visible = true;
            if (F_EraeeDars.Visible == true)
                F_EraeeDars.Visible = false;
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
