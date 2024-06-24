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
    public partial class Form_Daneshjo_Pardakht_Front : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;
        public Form_Daneshjo_Pardakht_Front()
        {
            InitializeComponent();
        }
        private void Form_Daneshjo_Pardakht_Front_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();

            Adapter = new SqlDataAdapter("select Shahriye from Table_Student_Info where StudentId like '" + Class1.Student_Code + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            txtMablaghKol.Text = Dt.Rows[0][0].ToString();
            Class1.StudentShahriye_Koll += Convert.ToInt32(Dt.Rows[0][0]);
        }
        private void btnPardakht_Click(object sender, EventArgs e)
        {
            Class1.StudentShahriye = Convert.ToInt32(txtSabet.Text);
            new Form_Daneshjo_Pardakht_Pardakht_Front().ShowDialog();
            this.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Daneshjo_Vorod_Front().Show();
            this.Close();
        }
    }
}
