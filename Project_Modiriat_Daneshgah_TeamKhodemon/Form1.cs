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
    public partial class Form1 : Form
    {
        public SqlConnection Con;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_VorodDaneshjo_Click(object sender, EventArgs e)
        {
            new Form_Daneshjo_Login().Show();
            this.Hide();
        }

        private void btn_VorodOstad_Click(object sender, EventArgs e)
        {
            new Form_Ostad_Login().Show();
            this.Hide();
        }

        private void btn_VorodModir_Click(object sender, EventArgs e)
        {
            new Form_Modir_Login().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Top = 200;
            this.Left = 400;
            //Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //Con.Open();
            //SqlCommand com = new SqlCommand("Update Table_Admin_Lesson set TakenForTeacher=0 where EraeeCode=200 ", Con);
            //SqlDataReader dr = com.ExecuteReader();

        }

        private void BtnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
