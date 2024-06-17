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
    public partial class Form_Modir_Daneshjo_AddNewStudent : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;
        public Form_Modir_Daneshjo_AddNewStudent()
        {
            InitializeComponent();
        }

        private void Form_Modir_Daneshjo_AddNewStudent_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select * from Table_Student_Info ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Modir_Daneshjo().Form_Modir_Daneshjo_Load(sender, e);
            new Form_Modir_Daneshjo().Show();

            this.Close();
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {

            if ( txtStudent_FName.Text.Trim() == ""||  txtStudent_LName.Text.Trim() == ""||
            txtStudent_Code.Text.Trim() == ""||txtStudent_FatherName.Text.Trim() == ""||
            txtStudent_NationalCode.Text.Trim() == ""||txtStudent_Maghta.Text.Trim() == ""||
            txtStudent_BirthDate.Text.Trim() == ""||txtStudent_EnterDate.Text.Trim() == ""||
            txtStudent_Reshte.Text.Trim() == ""||txtStudent_Shahtiye.Text.Trim() == ""||
            txtStudent_Phone.Text.Trim() == ""||txtStudent_Password.Text.Trim() == ""||
            txtStudent_NubmerTerm.Text.Trim() == ""||txtStudent_Address.Text.Trim() == "")
            {
                MessageBox.Show("مقادیر خالی را پر کنید !!!",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    DataRow NewRow = Dt.NewRow();

                    NewRow["StudentId"] = Convert.ToDecimal(txtStudent_Code.Text);
                    NewRow["FirstName"] = txtStudent_FName.Text;
                    NewRow["LastName"] = txtStudent_LName.Text;
                    NewRow["FatherName"] = txtStudent_FatherName.Text;
                    NewRow["NationalCode"] = Convert.ToDecimal(txtStudent_NationalCode.Text);
                    NewRow["BirhDate"] = txtStudent_BirthDate.Text;
                    NewRow["Maghta"] = txtStudent_Maghta.Text;
                    NewRow["Address"] = txtStudent_Address.Text;
                    NewRow["Phone"] = Convert.ToDecimal(txtStudent_Phone.Text);
                    NewRow["Password"] = Convert.ToDecimal(txtStudent_Password.Text);
                    NewRow["InterDate"] = txtStudent_EnterDate.Text;
                    NewRow["Shahriye"] = txtStudent_Shahtiye.Text;
                    NewRow["NumberTerm"] = txtStudent_NubmerTerm.Text;
                    NewRow["Reshte"] = txtStudent_Reshte.Text;


                    Dt.Rows.Add(NewRow);
                    SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
                    Adapter.Update(Dt);
                    Dt.AcceptChanges();

                    DialogResult dr = MessageBox.Show("اطلاعات دانشجو با موفقیت ثبت شد",
                                      "اطلاعیه!!!",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {

                        txtStudent_FName.Clear();
                        txtStudent_LName.Clear();
                        txtStudent_Code.Clear();
                        txtStudent_FatherName.Clear();
                        txtStudent_NationalCode.Clear();
                        txtStudent_Maghta.SelectedIndex = -1;
                        txtStudent_BirthDate.Clear();
                        txtStudent_EnterDate.Clear();
                        txtStudent_Reshte.SelectedIndex = -1;
                        txtStudent_Shahtiye.Clear();
                        txtStudent_Phone.Clear();
                        txtStudent_Password.Clear();
                        txtStudent_NubmerTerm.Clear();
                        txtStudent_Address.Clear();
                        txtStudent_FName.Focus();
                    }

                }
                catch
                {
                    DialogResult dr = MessageBox.Show("کد دانشجویی نامعتبر است - دانشجوی دیگری با این کد در حال فعالیت هستند",
                                        "اخطار در ثبت اطلاعات!!!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        txtStudent_FName.Clear();
                        txtStudent_LName.Clear();
                        txtStudent_Code.Clear();
                        txtStudent_FatherName.Clear();
                        txtStudent_NationalCode.Clear();
                        txtStudent_Maghta.SelectedIndex = -1;
                        txtStudent_BirthDate.Clear();
                        txtStudent_EnterDate.Clear();
                        txtStudent_Reshte.SelectedIndex = -1;
                        txtStudent_Shahtiye.Clear();
                        txtStudent_Phone.Clear();
                        txtStudent_Password.Clear();
                        txtStudent_NubmerTerm.Clear();
                        txtStudent_Address.Clear();
                        txtStudent_FName.Focus();
                    }
                }
            }
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
                lbllStudentCode.Text = "********";
            }
            else
            {
                lbllStudentCode.Text = "";
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

        private void txtStudent_EnterDate_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_EnterDate.Text.Trim() == "")
            {
                lblEnterDate.Text = "********";
            }
            else
            {
                lblEnterDate.Text = "";
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

        private void txtStudent_Shahtiye_TextChanged(object sender, EventArgs e)
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

        private void txtStudent_Password_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_Password.Text.Trim() == "")
            {
                lblPassword.Text = "********";
            }
            else
            {
                lblPassword.Text = "";
            }
        }

        private void txtStudent_NubmerTerm_TextChanged(object sender, EventArgs e)
        {
            if (txtStudent_NubmerTerm.Text.Trim() == "")
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
                lblAddress.Text = "********";
            }
            else
            {
                lblAddress.Text = "";
            }
        }

        private void txtStudent_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtStudent_Code.Text.Length >= 14)
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

        private void txtStudent_Shahtiye_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
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

        private void txtStudent_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtStudent_Password.Text.Length >= 14)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtStudent_NubmerTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtStudent_NubmerTerm.Text.Length >= 2)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void btnNewList_Click(object sender, EventArgs e)
        {
            //    txtStudent_FName.Clear();
            //    txtStudent_LName.Clear();
            //    txtStudent_Code.Clear();
            //    txtStudent_FatherName.Clear();
            //    txtStudent_NationalCode.Clear();
            //    txtStudent_Maghta=null;
            //    txtStudent_BirthDate.Clear();
            //    txtStudent_EnterDate.Clear();
            //    txtStudent_Reshte=null;
            //    txtStudent_Shahtiye.Clear();
            //    txtStudent_Phone.Clear();
            //    txtStudent_Password.Clear();
            //    txtStudent_NubmerTerm.Clear();
            //    txtStudent_Address.Clear();

            this.Visible = false;
            new Form_Modir_Daneshjo_AddNewStudent().Show();
        }
    }
}
