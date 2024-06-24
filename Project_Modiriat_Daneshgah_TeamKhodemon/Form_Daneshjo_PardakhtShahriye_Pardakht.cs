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
    public partial class Form_Daneshjo_PardakhtShahriye_Pardakht : Form
    {
        public SqlConnection Con;

        int minute = 1;
        int second = 0;
        public Form_Daneshjo_PardakhtShahriye_Pardakht()
        {
            InitializeComponent();
        }

        private void Form_Daneshjo_PardakhtShahriye_Pardakht_Load(object sender, EventArgs e)
        {
            this.Left = 220;
            this.Top = 136;

            timer1.Enabled = true;
            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();

            txtMablagh.Text = Class1.StudentShahriye.ToString();

            Random pass = new Random();
            txtRandomCode.Text = pass.Next(10000, 99999).ToString();

        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            if (txtShomareKart.Text.Trim() == "" || txtCVV2.Text.Trim() == ""
                || txtEngheza.Text.Trim() == "" || txtRandomCode_Student.Text.Trim() == ""
                || txtRamz2.Text.Trim() == "")
            {
                DialogResult dr = MessageBox.Show("مقادیر خالی را پر کنید !!!",
                                                                       " خطا در پرداخت وجه !!! ",
                                                                       MessageBoxButtons.OK,
                                                                       MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    Random pass = new Random();
                    txtRandomCode.Text = pass.Next(10000, 99999).ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(txtRandomCode.Text) == Convert.ToInt32(txtRandomCode_Student.Text))
                {
                    DialogResult dr = MessageBox.Show("پرداخت با موفقیت انجام شد",
                                                       " اطلاعیه !!!",
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information);

                    Class1.StudentShahriye_Koll += Class1.StudentShahriye;
                    timer1.Enabled = false;
                    if (dr == DialogResult.OK)
                    {
                        SqlCommand Com1 = new SqlCommand("update Table_Student_Info set Shahriye='" + Class1.StudentShahriye_Koll + "' where StudentId='" + Class1.Student_Code + "'   ", Con);
                        Com1.ExecuteNonQuery();
                        this.Close();
                        new Form_Daneshjo_Vorood().Show();
                        new Form_Daneshjo_PardakhtShahriye().Show();
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("عبارت داخل کادر را بدرسی وارد کنید !!!",
                                                        " خطا در پرداخت وجه !!! ",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        Random pass = new Random();
                        txtRandomCode.Text = pass.Next(10000, 99999).ToString();
                        txtRandomCode_Student.Clear();
                    }
                }
            }

        }

        private void txtCVV2_TextChanged(object sender, EventArgs e)
        {
            if (txtCVV2.Text.Trim() == "")
            {
                lblCVV2.Text = "********";
            }
            else
            {
                lblCVV2.Text = "";
            }
        }

        private void txtRamz2_TextChanged(object sender, EventArgs e)
        {
            if (txtRamz2.Text.Trim() == "")
            {
                lblRamz2.Text = "********";
            }
            else
            {
                lblRamz2.Text = "";
            }
        }

        private void txtCVV2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (minute > 0)
            {
                lblTimer.Text = minute.ToString() + " : " + second.ToString();
                minute--;
                second = 59;
                if (minute == 0)
                {
                    lblTimer.Text = minute.ToString() + " : " + second.ToString();
                }
            }
            if (second >= 0)
            {
                lblTimer.Text = minute.ToString() + " : " + second.ToString();
                second--;
                if (second == 0)
                {
                    lblTimer.Text = minute.ToString() + " : " + second.ToString();
                    DialogResult dr = MessageBox.Show("زمان شما به اتمام رسید",
                                                      "اخطار", 
                                                      MessageBoxButtons.OK,
                                                      MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        new Form_Daneshjo_PardakhtShahriye().Show();
                        this.Visible = false;
                    }
                }
            }
        }
    }
}
