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
    public partial class Form_Daneshjo_Login : Form
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

        public Form_Daneshjo_Login()
        {
            InitializeComponent();
        }

        private void GetIndexStudent()
        {
            for (int i = 0; i < DtIndex.Rows.Count; i++)
            {
                DataRow NewRow = DtIndex.Rows[i];
                if (NewRow["StudentId"].ToString() == txtStudentCode.Text)
                    Index = i;
            }
        }
        private void GetInfoStudent()
        {
            Class1.Student_Code = Convert.ToInt32(DtIndex.Rows[Index][0]);
            Class1.Student_FirstName = (string)DtIndex.Rows[Index][1];
            Class1.Student_LastName = (string)DtIndex.Rows[Index][2];
            Class1.Student_FathersName = (string)DtIndex.Rows[Index][3];
            Class1.StudentNationalCode = Convert.ToInt32(DtIndex.Rows[Index][4]);
            Class1.Student_DateOfBirth = (string)DtIndex.Rows[Index][5];
            Class1.Student_EnterDate = (string)DtIndex.Rows[Index][6];
            Class1.Student_Reshteh = (string)DtIndex.Rows[Index][7];
            Class1.StudentMaghta = (string)DtIndex.Rows[Index][8];
            Class1.Student_Term = Convert.ToInt32(DtIndex.Rows[Index][10]);
        }

        private void Form_Daneshjo_Login_Load(object sender, EventArgs e)
        {
            this.Top = 120;
            this.Left = 250;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }

        private void btnVorod_Click(object sender, EventArgs e)
        {
            try
            {
                Adapter = new SqlDataAdapter("select * from Table_Student_Info where StudentId='" + Convert.ToInt32(txtStudentCode.Text) + "' and Password='" + Convert.ToInt32(txtStudetnPassword.Text) + "'", Con);
                Dt = new DataTable();
                Adapter.Fill(Dt);
                Bin = new BindingSource();
                Bin.DataSource = Dt;



                AdapterIndex = new SqlDataAdapter("select * from Table_Student_Info", Con);
                DtIndex = new DataTable();
                AdapterIndex.Fill(DtIndex);
                BinIndex = new BindingSource();
                BinIndex.DataSource = DtIndex;

                if (Dt.Rows.Count > 0)
                {
                    GetIndexStudent();
                    GetInfoStudent();

                    new Form_Daneshjo_Vorood().Visible = true;
                    this.Visible = false;
                }
                else
                {
                    AdapterCount = new SqlDataAdapter("select * from Table_Student_Info where StudentId='" + txtStudentCode.Text + "' ", Con);
                    DtCount = new DataTable();
                    AdapterCount.Fill(DtCount);
                    BinCount = new BindingSource();
                    BinCount.DataSource = DtCount;

                    txtStudentCode.Clear();
                    txtStudetnPassword.Clear();

                    if (DtCount.Rows.Count > 0)
                    {
                        lblPassword.Text = "* رمز عبور خود را اشتباه وارد کردید";
                        lblStudentCode.Text = "";
                    }
                    else
                    {
                        lblStudentCode.Text = "* نام کاربری خود را اشتباه وارد کردید";
                        lblPassword.Text = "";
                    }
                }
            }
            catch
            {
                DialogResult dr = MessageBox.Show("دانشجوی محترم\n کد دانشجویی و رمزعبور خود را وارد کنید ",
                                               "اخطار !!!",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error);
            }
        }


        private void txtStudetnPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void txtStudentCode_TextChanged(object sender, EventArgs e)
        {
            if (txtStudentCode.Text.Length > 0)
                lblStudentCode.Text = "";
        }

        private void txtStudetnPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtStudetnPassword.Text.Length > 0)
                lblPassword.Text = "";
        }

        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
