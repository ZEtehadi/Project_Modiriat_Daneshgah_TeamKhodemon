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
    public partial class Form_Modir_Daneshjo_Virayesh : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;


        public Form_Modir_Daneshjo_Virayesh()
        {
            InitializeComponent();
        }

        private void Form_Modir_Daneshjo_Virayesh_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;

            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select * from Table_Student_Info", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;

            txtStudent_Code.Text = Dt.Rows[Class1.IndexOfStudentSelect][0].ToString();
            txtStudent_FName.Text = Dt.Rows[Class1.IndexOfStudentSelect][1].ToString();
            txtStudent_LName.Text = Dt.Rows[Class1.IndexOfStudentSelect][2].ToString();
            txtStudent_FatherName.Text = Dt.Rows[Class1.IndexOfStudentSelect][3].ToString();
            txtStudent_NationalCode.Text = Dt.Rows[Class1.IndexOfStudentSelect][4].ToString();
            txtStudent_Maghta.Text = Dt.Rows[Class1.IndexOfStudentSelect][8].ToString();
            txtStudent_BirthDate.Text = Dt.Rows[Class1.IndexOfStudentSelect][5].ToString();
            txtStudent_Phone.Text = Dt.Rows[Class1.IndexOfStudentSelect][12].ToString();
            txtStudent_Reshte.Text = Dt.Rows[Class1.IndexOfStudentSelect][7].ToString();
            txtStudent_NumberTerm.Text = Dt.Rows[Class1.IndexOfStudentSelect][10].ToString();
            txtStudent_Address.Text = Dt.Rows[Class1.IndexOfStudentSelect][11].ToString();


        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (txtStudent_FName.Text.Trim() == " " || txtStudent_LName.Text.Trim() == ""
              || txtStudent_FatherName.Text.Trim() == " " || txtStudent_NationalCode.Text.Trim() == ""
              || txtStudent_BirthDate.Text.Trim() == ""
              || txtStudent_BirthDate.Text.Trim() == "" || txtStudent_Maghta.Text.Trim() == ""
              || txtStudent_Phone.Text.Trim() == "" || txtStudent_Reshte.Text.Trim() == ""
              || txtStudent_NumberTerm.Text.Trim() == "" || txtStudent_Address.Text.Trim() == "")
            {
                MessageBox.Show("مقادیر خالی را پر کنید !!!",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                Dt.Rows[Class1.IndexOfStudentSelect][1] = txtStudent_FName.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][2] = txtStudent_LName.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][3] = txtStudent_FatherName.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][4] = txtStudent_NationalCode.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][5] = txtStudent_BirthDate.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][8] = txtStudent_Maghta.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][12] = txtStudent_Phone.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][7] = txtStudent_Reshte.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][10] = txtStudent_NumberTerm.Text;
                Dt.Rows[Class1.IndexOfStudentSelect][11] = txtStudent_Address.Text;

                try
                {
  SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
                Adapter.Update(Dt);
                Dt.AcceptChanges();

                MessageBox.Show("تغییرات با موفقیت انجام شد",
                                "اطلاعیه!!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                }
                catch
                {
                    DialogResult dr = MessageBox.Show("ابتدا مشخصات خود را ویرایش کرده سپس رمزعبور را تغییر دهید",
                                      " خطا در اعمال ویرایش اطلاعات !!!",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        new Form_Modir_Daneshjo().Show();
                        this.Visible = false;
                    }
                }

            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
           // new Form_Modir_Daneshjo().Form_Modir_Daneshjo_Load(sender, e);
            new Form_Modir_Daneshjo().Show();

            this.Close();
        }

        private void txtStudent_FName_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_FName.Text.Trim() == "")
            {
                lblName.Text = "********";
            }
            else
            {
                lblName.Text = "";
            }
        }

        private void txtStudent_LName_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_LName.Text.Trim() == "")
            {
                lblLastName.Text = "********";
            }
            else
            {
                lblLastName.Text = "";
            }
        }

        private void txtStudent_Code_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_Code.Text.Trim() == "")
            {
                lblNationalCode.Text = "********";
            }
            else
            {
                lblNationalCode.Text = "";
            }
        }

        private void txtStudent_FatherName_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_FatherName.Text.Trim() == "")
            {
                lblFatherName.Text = "********";
            }
            else
            {
                lblFatherName.Text = "";
            }
        }

        private void txtStudent_NationalCode_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_NationalCode.Text.Trim() == "")
            {
                lblNationalCode.Text = "********";
            }
            else
            {
                lblNationalCode.Text = "";
            }
        }

        private void txtStudent_Maghta_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_Maghta.Text.Trim() == "")
            {
                lblMaghta.Text = "********";
            }
            else
            {
                lblMaghta.Text = "";
            }
        }

        private void txtStudent_BirthDate_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_BirthDate.Text.Trim() == "")
            {
                lblBirthDate.Text = "********";
            }
            else
            {
                lblBirthDate.Text = "";
            }
        }

        private void txtStudent_Phone_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_Phone.Text.Trim() == "")
            {
                lblPhone.Text = "********";
            }
            else
            {
                lblPhone.Text = "";
            }
        }

        private void txtStudent_Reshte_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_Reshte.Text.Trim() == "")
            {
                lblReshte.Text = "********";
            }
            else
            {
                lblReshte.Text = "";
            }
        }

        private void txtStudent_NumberTerm_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_NumberTerm.Text.Trim() == "")
            {
                lblNumberTerm.Text = "********";
            }
            else
            {
                lblNumberTerm.Text = "";
            }
        }

        private void txtStudent_Address_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_Address.Text.Trim() == "")
            {
                lbladressss.Text = "********";
            }
            else
            {
                lbladressss.Text = "";
            }
        }

        private void txtStudent_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtStudent_Phone.Text.Length >= 11)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtStudent_NationalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtStudent_NationalCode.Text.Length >= 10)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtStudent_NumberTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtStudent_NumberTerm.Text.Length >= 2)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void btnOstad_ChangePassword_Click(object sender, EventArgs e)
        {
            new Form_Modir_Daneshjo_Virayesh_ChangePassowrd().ShowDialog();
        }

     
    }
}
