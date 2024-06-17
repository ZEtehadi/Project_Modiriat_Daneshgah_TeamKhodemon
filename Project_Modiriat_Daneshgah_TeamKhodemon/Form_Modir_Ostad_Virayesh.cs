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
    public partial class Form_Modir_Ostad_Virayesh : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public int indexOstadSelect = Class1.IndexOfTeacherSelect;
        public Form_Modir_Ostad_Virayesh()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Ostad().Form_Modir_Ostad_Load(sender, e);
            new Form_Modir_Ostad().Show();
            // this.Hide();

            //Class1.F_Ostad.Form_Modir_Ostad_Load(sender, e);
            this.Close();


        }

        private void Form_Modir_Ostad_Virayesh_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR Z_E\\MSSQLSERVER_2022
            Con.Open();

            Adapter = new SqlDataAdapter("select * from Table_Teacher_Info", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;

            // Class1.OstadSelectCode = Convert.ToInt32(Dt.Rows[indexOstadSelect][0]);
            Class1.OstadSelecetPassword = Convert.ToDecimal(Dt.Rows[indexOstadSelect][9]);

            txtOstad_Code.Text = Dt.Rows[indexOstadSelect][0].ToString();
            txtOstad_FName.Text = Dt.Rows[indexOstadSelect][1].ToString();
            txtOstad_LName.Text = Dt.Rows[indexOstadSelect][2].ToString();
            txtOstad_FatherName.Text = Dt.Rows[indexOstadSelect][3].ToString();
            txtOstad_NationalCode.Text = Dt.Rows[indexOstadSelect][4].ToString();
            txtOstad_BirthDate.Text = Dt.Rows[indexOstadSelect][5].ToString();
            txtOstad_Madrak.Text = Dt.Rows[indexOstadSelect][6].ToString();
            txtOstad_Address.Text = Dt.Rows[indexOstadSelect][7].ToString();
            txtOstad_Phone.Text = Dt.Rows[indexOstadSelect][8].ToString();
            //txtPassword.Text = Dt.Rows[indexOstadSelect][9].ToString();

            Class1.OstadSelectCode = Convert.ToDecimal(txtOstad_Code.Text);

        }

        private void btnOstad_ChangePassword_Click(object sender, EventArgs e)
        {
            new Form_Modir_Ostad_Virayesh_ChangePassword().ShowDialog();
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {

            //DataRow Newrow = Dt.NewRow();
            if (txtOstad_FName.Text.Trim() == "" || txtOstad_LName.Text.Trim() == ""
                || txtOstad_FatherName.Text.Trim() == "" ||  txtOstad_NationalCode.Text.Trim() == "" 
                || txtOstad_BirthDate.Text.Trim() == ""
                || txtOstad_Address.Text.Trim() == "" || txtOstad_Madrak.Text.Trim() == ""
                || txtOstad_Phone.Text.Trim() == "")
            {
                MessageBox.Show("مقادیر خالی را پر کنید !!!",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                Dt.Rows[indexOstadSelect][1] = txtOstad_FName.Text;
                Dt.Rows[indexOstadSelect][2] = txtOstad_LName.Text;
                Dt.Rows[indexOstadSelect][3] = txtOstad_FatherName.Text;
                Dt.Rows[indexOstadSelect][4] = txtOstad_NationalCode.Text;
                Dt.Rows[indexOstadSelect][5] = txtOstad_BirthDate.Text;
                Dt.Rows[indexOstadSelect][6] = txtOstad_Madrak.Text;
                Dt.Rows[indexOstadSelect][7] = txtOstad_Address.Text;
                Dt.Rows[indexOstadSelect][8] = txtOstad_Phone.Text;

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
                  DialogResult dr= MessageBox.Show("ابتدا مشخصات خود را ویرایش کرده سپس رمزعبور را تغییر دهید",
                                    " خطا در اعمال ویرایش اطلاعات !!!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        new Form_Modir_Ostad().Show();
                        this.Visible = false;
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
            if (txtOstad_NationalCode.Text.Trim() == " ")
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
        private void txtOstad_BirthDate_TextChanged(object sender, EventArgs e)
        {
            if (txtOstad_BirthDate.Text.Trim() == "")
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
            if (txtOstad_Phone.Text.Trim() == "")
            {
                lblPhone.Text = "********";
            }
            else
            {
                lblPhone.Text = "";
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
    }
}
