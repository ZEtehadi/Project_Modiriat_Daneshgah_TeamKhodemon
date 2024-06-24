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
using System.Data.SqlClient;

namespace Project_Modiriat_Daneshgah_TeamKhodemon
{
    public partial class Form_Ostad_login_Front : Form
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

        public Form_Ostad_login_Front()
        {
            InitializeComponent();
        }
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


        private void Form_Ostad_login_Front_Load(object sender, EventArgs e)
        {
            this.Top = 120;
            this.Left = 250;

            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR .
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

                    new Form_Ostad_Vorod_Front().Visible = true;
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
            new Form1_Front().Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void txtOstadCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void txtOstadPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                call();
            }
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
                        Form_Ostad_Vorod_Front n = new Form_Ostad_Vorod_Front();
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
                Adapter = new SqlDataAdapter("select * from Table_Teacher_Info where TeacherCode='" + Convert.ToInt32(txt_Code_SMS.Text) + "'", Con);
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


                    checklogin();
                }
                else
                {
                    DialogResult dr = MessageBox.Show(" کد استاد را اشتباه وارد کردید !! ",
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
                DialogResult dr = MessageBox.Show(" مدیر محترم\n کد استاد و رمزعبور خود را وارد کنید ",
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

        private void txt_PhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
