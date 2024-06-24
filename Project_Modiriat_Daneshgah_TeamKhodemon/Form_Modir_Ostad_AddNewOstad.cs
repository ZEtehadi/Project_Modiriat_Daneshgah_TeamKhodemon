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
    public partial class Form_Modir_Ostad_AddNewOstad : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public Form_Modir_Ostad_AddNewOstad()
        {
            InitializeComponent();
        }

        private void Form_Modir_Ostad_AddNewOstad_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();
            Adapter = new SqlDataAdapter("select * from Table_Teacher_Info ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // new Form_Modir_Ostad().Form_Modir_Ostad_Load(sender, e);
            new Form_Modir_Ostad().Show();
            //this.Hide();

            //Class1.F_Ostad.Form_Modir_Ostad_Load(sender, e);
            this.Close();

        }

        private void lblBirthDate_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (txtOstad_FName.Text.Trim() == "" || txtOstad_LName.Text.Trim() == ""
               || txtOstad_FatherName.Text.Trim() == "" || txtOstad_NationalCode.Text.Trim() == ""
               || txtOstad_BirthDate.Text.Trim() == "" || txtOstad_Password.Text.Trim() == ""
               || txtOstad_Address.Text.Trim() == "" || txtOstad_Madrak.Text.Trim() == ""
               || txtOstad_Phone.Text.Trim() == "" || txtOstad_Code.Text.Trim() == "")
            {
                MessageBox.Show("مقادیر خالی را پر کنید !!!",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                DataRow NewRow = Dt.NewRow();

                NewRow["TeacherCode"] = txtOstad_Code.Text;
                NewRow["FirstName"] = txtOstad_FName.Text;
                NewRow["LastName"] = txtOstad_LName.Text;
                NewRow["FatherName"] = txtOstad_FatherName.Text;
                NewRow["NationalCode"] = txtOstad_NationalCode.Text;
                NewRow["BirthDate"] = txtOstad_BirthDate.Text;
                NewRow["Madrak"] = txtOstad_Madrak.Text;
                NewRow["Address"] = txtOstad_Address.Text;
                NewRow["Phone"] = txtOstad_Phone.Text;
                NewRow["Password"] = txtOstad_Password.Text;

                Dt.Rows.Add(NewRow);
                try
                {
                    SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
                    Adapter.Update(Dt);
                    Dt.AcceptChanges();

                    DialogResult dr = MessageBox.Show("اطلاعات استاد با موفقیت ثبت شد",
                                     "اطلاعیه!!!",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        txtOstad_FName.Clear();
                        txtOstad_LName.Clear();
                        txtOstad_Code.Clear();
                        txtOstad_FatherName.Clear();
                        txtOstad_NationalCode.Clear();
                        txtOstad_Madrak.SelectedIndex = -1;
                        txtOstad_BirthDate.Clear();
                        txtOstad_Phone.Clear();
                        txtOstad_Password.Clear();
                        txtOstad_Address.Clear();
                        txtOstad_FName.Focus();
                    }
                }
                catch
                {
                    DialogResult dr = MessageBox.Show("کد استاد نامعتبر است - استاد دیگری با این کد در حال فعالیت هستند",
                                    "اخطار در ثبت اطلاعات!!!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        txtOstad_FName.Clear();
                        txtOstad_LName.Clear();
                        txtOstad_Code.Clear();
                        txtOstad_FatherName.Clear();
                        txtOstad_NationalCode.Clear();
                        txtOstad_Madrak.SelectedIndex = -1;
                        txtOstad_BirthDate.Clear();
                        txtOstad_Phone.Clear();
                        txtOstad_Password.Clear();
                        txtOstad_Address.Clear();
                        txtOstad_FName.Focus();
                    }
                }
            }
        }

        private void txtOstad_FName_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_FName.Text.Trim() == "")
            {
                lblName.Text = "********";
            }
            else
            {
                lblName.Text = "";
            }
        }

        private void txtOstad_LName_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_LName.Text.Trim() == "")
            {
                lblLastName.Text = "********";
            }
            else
            {
                lblLastName.Text = "";
            }
        }

        private void txtOstad_Code_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_Code.Text.Trim() == "")
            {
                lbllOstadCode.Text = "********";
            }
            else
            {
                lbllOstadCode.Text = "";
            }
        }

        private void txtOstad_FatherName_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_FatherName.Text.Trim() == "")
            {
                lblFatherName.Text = "********";
            }
            else
            {
                lblFatherName.Text = "";
            }
        }

        private void txtOstad_NationalCode_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_NationalCode.Text.Trim() == "")
            {
                lblNationalCode.Text = "********";
            }
            else
            {
                lblNationalCode.Text = "";
            }
        }

        private void txtOstad_Madrak_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_Madrak.Text.Trim() == "")
            {
                lblMadrak.Text = "********";
            }
            else
            {
                lblMadrak.Text = "";
            }
        }
        private void txtOstad_BirthDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (txtOstad_BirthDate.Text.Trim() == "")//.Trim() == ""
            {
                lblBirthDate.Text = "********";
            }
            else
            {
                lblBirthDate.Text = "";
            }
        }

        private void txtOstad_NationalCode_TextChanged_1(object sender, EventArgs e)
        {

            if (txtOstad_NationalCode.Text.Trim() == "")
            {
                lblNationalCode.Text = "********";
            }
            else
            {
                lblNationalCode.Text = "";
            }
        }
        private void txtOstad_BirthDate_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_BirthDate.Text==null)//.Trim() == ""
            {
                lblBirthDate.Text = "********";
            }
            else
            {
                lblBirthDate.Text = "";
            }
        }

        private void txtOstad_Phone_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_Phone.Text.Trim() == "" || txtOstad_Phone.Text.Length < 11)
            {
                lblPhone.Text = "********";
            }
            else
            {
                lblPhone.Text = "";
            }


        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_Password.Text.Trim() == "")
            {
                lblPassword.Text = "********";
            }
            else
            {
                lblPassword.Text = "";
            }
        }

        private void txtOstad_Address_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_Address.Text.Trim() == "")
            {
                lblAddress.Text = "********";
            }
            else
            {
                lblAddress.Text = "";
            }
        }



        private void txtOstad_Code_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtOstad_Code.Text.Length >= 14)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtOstad_Password_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtOstad_Password.Text.Length >= 14)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtOstad_NationalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtOstad_NationalCode.Text.Length >= 10)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtOstad_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtOstad_Phone.Text.Length >= 11)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

     
        //        if (char.IsDigit(e.KeyChar))
        //            {
        //                e.Handled = false;
        //            }
        //            else
        //            {
        //                e.Handled = true;
        //            }
        //if (txtOstad_Phone.Text.Length >= 11)
        //{
        //    e.Handled = true;
        //}
        //if (e.KeyChar == (char)Keys.Back)
        //{
        //    e.Handled = false;
        //}
    }
}
