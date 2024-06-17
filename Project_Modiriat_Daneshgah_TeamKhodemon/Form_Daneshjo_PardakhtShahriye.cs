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
    public partial class Form_Daneshjo_PardakhtShahriye : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;
        public Form_Daneshjo_PardakhtShahriye()
        {
            InitializeComponent();
        }

        private void Form_Daneshjo_PardakhtShahriye_Load(object sender, EventArgs e)
        {
            this.Left = 220;
            this.Top = 136;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();

            Adapter = new SqlDataAdapter("select Shahriye from Table_Student_Info where StudentId like '" + Class1.Student_Code + "'",Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            txtMablaghKol.Text = Dt.Rows[0][0].ToString();
            Class1.StudentShahriye_Koll += Convert.ToInt32(Dt.Rows[0][0]);


        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Daneshjo_Vorood().Show();
            this.Hide();
        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            Class1.StudentShahriye = Convert.ToInt32(txtSabet.Text);
            new Form_Daneshjo_PardakhtShahriye_Pardakht().ShowDialog();
            this.Visible = false;
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            new Form_Daneshjo_Vorood().Show();
            this.Close();
        }
    }
}
