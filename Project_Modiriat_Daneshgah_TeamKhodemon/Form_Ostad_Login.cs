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
    public partial class Form_Ostad_Login : Form
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
        private void GetIndexOstad()
        {
            for (int i = 0; i < DtIndex.Rows.Count; i++)
            {
                DataRow NewRow = DtIndex.Rows[i];
                if (NewRow["TeacherCode"].ToString() == txtOstadCode.Text)
                    Index = i;
            }
        }
        private void GetInfoOstad()
        {
            Class1.TeachCode = Convert.ToInt32(DtIndex.Rows[Index][0]);
            Class1.TeachFather = (string)DtIndex.Rows[Index][3];
            Class1.TeachName = (string)DtIndex.Rows[Index][1];
            Class1.TeachLastName = (string)DtIndex.Rows[Index][2];
            Class1.TeachMadrak = (string)DtIndex.Rows[Index][6];
            Class1.TeachNationalCode = Convert.ToInt32(DtIndex.Rows[Index][4]);
            Class1.TeachPhone = Convert.ToDecimal(DtIndex.Rows[Index][8]);
        }

        public Form_Ostad_Login()
        {
            InitializeComponent();
        }

        private void Form_Ostad_Login_Load(object sender, EventArgs e)
        {
            this.Top = 220;
            this.Left = 450;

            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR Z_E\\MSSQLSERVER_2022
            Con.Open();
        }
        public void call()
        {
            try
            {
                Adapter = new SqlDataAdapter("select * from Table_Teacher_Info where TeacherCode='" + txtOstadCode.Text + "' and Password='" + txtOstadPassword.Text + "'", Con);
                Dt = new DataTable();
                Adapter.Fill(Dt);
                Bin = new BindingSource();
                Bin.DataSource = Dt;

                AdapterIndex = new SqlDataAdapter("select * from Table_Teacher_Info", Con);
                DtIndex = new DataTable();
                AdapterIndex.Fill(DtIndex);
                BinIndex = new BindingSource();
                BinIndex.DataSource = DtIndex;

                if (Dt.Rows.Count > 0)
                {
                    GetIndexOstad();
                    GetInfoOstad();

                    new Form_Ostad_Vorod().Visible = true;
                    this.Visible = false;
                }
                else
                {
                    AdapterCount = new SqlDataAdapter("select * from Table_Teacher_Info where TeacherCode='" + txtOstadCode.Text + "' ", Con);
                    DtCount = new DataTable();
                    AdapterCount.Fill(DtCount);
                    BinCount = new BindingSource();
                    BinCount.DataSource = DtCount;

                    txtOstadCode.Clear();
                    txtOstadPassword.Clear();

                    if (DtCount.Rows.Count > 0)
                    {
                        lblPassword.Text = "* رمز عبور خود را اشتباه وارد کردید";
                        lblOstadCode.Text = "";
                    }
                    else
                    {

                        lblOstadCode.Text = "* نام کاربری خود را اشتباه وارد کردید";
                        lblPassword.Text = "";
                    }
                }
            }
            catch
            {
                DialogResult dr = MessageBox.Show("استاد محترم\n کد استاد و رمزعبور خود را وارد کنید ",
                                              "اخطار !!!",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
            }
        }
        private void btnVorod_Click(object sender, EventArgs e)
        {
            call();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }

        private void txtOstadCode_TextChanged(object sender, EventArgs e)
        {
            if (txtOstadCode.Text.Length > 0)
                lblOstadCode.Text = "";
        }

        private void txtOstadPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtOstadPassword.Text.Length > 0)
                lblOstadCode.Text = "";
        }


        private void txtOstadCode_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form_Ostad_Login_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtOstadPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                call();
            }
        }
    }
}
