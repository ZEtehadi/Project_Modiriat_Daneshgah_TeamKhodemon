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
    public partial class Form_Modir_Login : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter AdapterIndex;
        public BindingSource BinIndex;
        public DataTable DtIndex;

        public SqlDataAdapter AdapterCount;
        public BindingSource BinCount;
        public DataTable DtCount;

        public int Index;
        public Form_Modir_Login()
        {
            InitializeComponent();
        }


        private void GetIndexModir()
        {
            for (int i = 0; i < DtIndex.Rows.Count; i++)
            {
                DataRow NewRow = DtIndex.Rows[i];
                if (NewRow["AdminCode"].ToString() == txtModirCode.Text)
                    Index = i;
            }
        }

        private void GetInfoModir()
        {
            Class1.Modir_Code = Convert.ToInt32(DtIndex.Rows[Index][0]);
            Class1.Modir_FatherName = (string)DtIndex.Rows[Index][3];
            Class1.Modir_Fname = (string)DtIndex.Rows[Index][1];
            Class1.Modir_Lname = (string)DtIndex.Rows[Index][2];
            Class1.Modir_Madrak = (string)DtIndex.Rows[Index][6];
            Class1.Modir_NationalCode = Convert.ToInt32(DtIndex.Rows[Index][4]);
            Class1.Modir_Phone = Convert.ToDecimal(DtIndex.Rows[Index][8]);
        }



        private void Form_Modir_Login_Load(object sender, EventArgs e)
        {
            this.Top = 220;
            this.Left = 450;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();

        }


        private void btn_Vorod_Click(object sender, EventArgs e)
        {

            try
            {
                Adapter = new SqlDataAdapter("select * from Table_Admin_Info where AdminCode='" + Convert.ToInt32(txtModirCode.Text) + "' and Password='" + Convert.ToInt32(txtPassword.Text) + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;

            AdapterIndex = new SqlDataAdapter("select * from Table_Admin_Info", Con);
            DtIndex = new DataTable();
            AdapterIndex.Fill(DtIndex);
            BinIndex = new BindingSource();
            BinIndex.DataSource = DtIndex;

            if (Dt.Rows.Count > 0)
            {
                GetIndexModir();
                GetInfoModir();

                new Form_Modir_Vorood().Visible = true;
                this.Visible = false;
            }
            else
            {
                AdapterCount = new SqlDataAdapter("select * from Table_Admin_Info where AdminCode='" + txtModirCode.Text + "' ", Con);
                DtCount = new DataTable();
                AdapterCount.Fill(DtCount);
                BinCount = new BindingSource();
                BinCount.DataSource = DtCount;

                txtModirCode.Clear();
                txtPassword.Clear();

                if (DtCount.Rows.Count > 0) //DtCount.Rows.Count > 0
                {

                    lblPassword.Text = "* رمز عبور خود را اشتباه وارد کردید";
                    lblAdminCode.Text = "";
                    if (lblAdminCode.Text.Length > 0)
                        lblAdminCode.Text = "";
                }
                else
                {
        
                    lblAdminCode.Text= "* نام کاربری خود را اشتباه وارد کردید";
                    lblPassword.Text = "";
                    if (lblPassword.Text.Length > 0)
                        lblPassword.Text = "";
                }

            }
            }
            catch
            {
                DialogResult dr = MessageBox.Show(" مدیر محترم\n کد مدیریت و رمزعبور خود را وارد کنید ",
                                                      "اخطار !!!",
                                                      MessageBoxButtons.OK,
                                                      MessageBoxIcon.Error);
            }
        }

        private void bntBack_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }


        private void txtModirCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void txtModirCode_TextChanged(object sender, EventArgs e)
        {
            if (txtModirCode.Text.Length > 0)
                lblAdminCode.Text = "";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length > 0)
                lblPassword.Text = "";
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
