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
    public partial class Form_Modir_Vorood : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        Form_Modir_Ostad F_Ostad = new Form_Modir_Ostad();
        Form_Modir_Daneshjo F_Daneshjo = new Form_Modir_Daneshjo();
        Form_Modir_Dars F_Dars = new Form_Modir_Dars();
        public Form_Modir_Vorood()
        {
            InitializeComponent();
        }


        private void Form_Modir_Vorod_Load(object sender, EventArgs e)
        {
            this.Left = 210;
            this.Top = 95;

            txtModir_FName.Text = Class1.Modir_Fname.ToString();
            txtModir_LName.Text = Class1.Modir_Lname.ToString();
            txtModir_FatherName.Text = Class1.Modir_FatherName.ToString();
            txtModir_Code.Text = Class1.Modir_Code.ToString();
            txtModir_Madrak.Text = Class1.Modir_Madrak.ToString();
            txtModir_NationalCode.Text = Class1.Modir_NationalCode.ToString();
            txtModir_Phone.Text = Class1.Modir_Phone.ToString();

        }

        private void bntOstad_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Ostad().ShowDialog();
            try
            {
                F_Ostad.Visible = true;
                if (F_Daneshjo.Visible == true)
                    F_Daneshjo.Visible = false;
                if (F_Dars.Visible == true)
                    F_Dars.Visible = false;
            }
            catch { }

        }

        private void btnDaneshjo_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Daneshjo().ShowDialog();
            try
            {
                F_Daneshjo.Visible = true;
                if (F_Ostad.Visible == true)
                    F_Ostad.Visible = false;
                if (F_Dars.Visible == true)
                    F_Dars.Visible = false;
            }
            catch { }


        }

        private void bntDars_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Dars().ShowDialog();
            try
            {
                F_Dars.Visible = true;
                if (F_Ostad.Visible == true)
                    F_Ostad.Visible = false;
                if (F_Daneshjo.Visible == true)
                    F_Daneshjo.Visible = false;
            }
            catch { }
        }

        private void bntBack_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا از خروج اطمینان دارید ؟؟",
                                               "اطلاعیه !!!",
                                               MessageBoxButtons.YesNoCancel,
                                               MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                new Form1().Show();
                this.Close();
            }
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
