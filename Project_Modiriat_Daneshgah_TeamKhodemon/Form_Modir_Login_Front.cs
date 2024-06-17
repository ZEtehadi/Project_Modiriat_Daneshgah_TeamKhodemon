using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Net.Sockets;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data.SqlClient;

namespace Project_Modiriat_Daneshgah_TeamKhodemon
{
    public partial class Form_Modir_Login_Front : Form
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


        string mob;

        public Form_Modir_Login_Front()
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

        private void Form_Modir_Login_Front_Load(object sender, EventArgs e)
        {
            this.Top = 120;
            this.Left = 250;

            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR Z_E\\MSSQLSERVER_2022
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

                    new Form_Modir_Vorod_Front().Visible = true;
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

                        lblAdminCode.Text = "* نام کاربری خود را اشتباه وارد کردید";
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
            new Form1_Front().Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void lbl_FaramoshiRamz_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        public async void checklogin()
        {
            try
            {


                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.mrapi.ir/V2/sms/send");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authentication", "5MTMkdDZmN2YjRTYyAjYxQDNzczY3gjYhFjMkNGNxAjOzkjMwATNwIjM");
                httpWebRequest.Headers["Authentication"] = "5MTMkdDZmN2YjRTYyAjYxQDNzczY3gjYhFjMkNGNxAjOzkjMwATNwIjM";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"PhoneNumber\" : \"" + txt_PhoneNumber.Text + "\" ,\"PatternID\" : \"d0484a6c-54d1-4059-8467-aac16d06ed30\"  , \"Token\" :  \"Wm91ZmFuQ28xMjcxMjExMjcwZEc5eVlXNXFhWFF1YVhJdWVtOTFabUZ1WTI4PQ==\", \"ProjectType\":0}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (result == "{\"IsSuccess\":true,\"Code\":21,\"Message\":\"2درخواست با موفقیت انجام شد\",\"Data\":null}")
                    {
                        MessageBox.Show("کد ارسال شد.");
                        button1.Visible = false;
                        verifybtn.Visible = true;
                        mob = txt_PhoneNumber.Text;
                        txt_PhoneNumber.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("مشکلی در ارسال ایجاد شده");
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        public async void checkverify()
        {
            try
            {


                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.mrapi.ir/V2/sms/verify");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authentication", "5MTMkdDZmN2YjRTYyAjYxQDNzczY3gjYhFjMkNGNxAjOzkjMwATNwIjM");
                httpWebRequest.Headers["Authentication"] = "5MTMkdDZmN2YjRTYyAjYxQDNzczY3gjYhFjMkNGNxAjOzkjMwATNwIjM";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"PhoneNumber\" : \"" + mob + "\" ,\"Code\" : " + txt_PhoneNumber.Text + "}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result == "{\"IsSuccess\":true,\"Code\":193,\"Message\":\"کد اعتبار سنجی معتبر است\",\"Data\":true}")
                    {
                        MessageBox.Show("ورود موفق بود");
                        Form_Modir_Vorod_Front n = new Form_Modir_Vorod_Front();
                        n.Show();
                    }
                    else
                    {
                        MessageBox.Show("کد نا معتبر است");
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Adapter = new SqlDataAdapter("select * from Table_Admin_Info where AdminCode='" + Convert.ToInt32(txt_Code_SMS.Text) + "'", Con);
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

                    checklogin();
                }
                else
                {
                    DialogResult dr = MessageBox.Show(" کد مدبربت را اشتباه وارد کردید !! ",
                                                       "اخطار !!!",
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Error);

                    txt_Code_SMS.Clear();
                    txt_PhoneNumber.Clear();
                    txt_Code_SMS.Focus();
                }
            }
            catch
            {
                DialogResult dr = MessageBox.Show(" مدیر محترم\n کد مدیریت و شماره تماس خود را وارد کنید ",
                                                      "اخطار !!!",
                                                      MessageBoxButtons.OK,
                                                      MessageBoxIcon.Error);


            }
        }

        private void verifybtn_Click(object sender, EventArgs e)
        {
            checkverify();
            //panel1.Visible = false;

        }

        private void lblAdminCode_Click(object sender, EventArgs e)
        {

        }

        private void labelX3_Click(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void labelX2_Click(object sender, EventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
