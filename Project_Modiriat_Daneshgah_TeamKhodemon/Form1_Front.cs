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
    public partial class Form1_Front : Form
    {
        public Form1_Front()
        {
            InitializeComponent();
        }

        private void Form1_Front_Load(object sender, EventArgs e)
        {
            this.Top = 100;
            this.Left = 200;
        }

        private void btn_VorodModir_Click(object sender, EventArgs e)
        {
            new Form_Modir_Login_Front().Show();
            this.Hide();
        }

        private void btn_VorodOstad_Click(object sender, EventArgs e)
        {
            new Form_Ostad_login_Front().Show();
            this.Hide();
        }

        private void btn_VorodDaneshjo_Click(object sender, EventArgs e)
        {
            new Form_Daneshjo_Login_Front().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
