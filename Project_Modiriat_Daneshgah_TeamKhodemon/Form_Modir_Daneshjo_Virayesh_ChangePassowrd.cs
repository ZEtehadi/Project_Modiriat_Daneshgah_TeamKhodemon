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
    public partial class Form_Modir_Daneshjo_Virayesh_ChangePassowrd : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public decimal SelectStudentId = Class1.Student_Id;
        public decimal SelectStudentPassword = Class1.Student_Password;
        public Form_Modir_Daneshjo_Virayesh_ChangePassowrd()
        {
            InitializeComponent();
        }

        private void Form_Modir_Daneshjo_Virayesh_ChangePassowrd_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            txtLastPassword.Text = SelectStudentPassword.ToString();

            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();

            txtLastPassword.Text = SelectStudentPassword.ToString();

        }

        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtNewPassword.Text.Length >= 14)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtAgainNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtAgainNewPassword.Text.Length >= 14)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void btnAcceptChange_Click(object sender, EventArgs e)
        {

            if (txtNewPassword.Text.Trim() == "" || txtAgainNewPassword.Text.Trim() == "")
            {
                if (txtNewPassword.Text.Trim() == "")
                    lblNewPassword.Text = "******* مقدار خالی را پر کنید ";
                else
                    lblNewPassword.Text = "";
                if (txtAgainNewPassword.Text.Trim() == "")
                    lblAcceptChange.Text = "******* مقدار خالی را پر کنید ";
                else
                    lblAcceptChange.Text = "******* مقدار خالی را پر کنید ";
            }
            else
            {

                if (txtNewPassword.Text.ToString() != txtAgainNewPassword.Text.ToString())
                {
                    lblAcceptChange.Text = "*** Not True";
                    txtAgainNewPassword.Clear();
                }
                else
                {
                    Adapter = new SqlDataAdapter("Update Table_Student_Info set Password='" + txtNewPassword.Text + "'where StudentId='" + SelectStudentId + "'", Con);
                    Dt = new DataTable();
                    Adapter.Fill(Dt);
                    Bin = new BindingSource();
                    Bin.DataSource = Dt;



                    MessageBox.Show("تغیر رمز با موفقیت انجام شد",
                                    "اطلاعیه",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    Class1.Student_Password = Convert.ToDecimal(txtNewPassword.Text);
                }
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Con.Close();
            this.Close();
        }
    }
}
