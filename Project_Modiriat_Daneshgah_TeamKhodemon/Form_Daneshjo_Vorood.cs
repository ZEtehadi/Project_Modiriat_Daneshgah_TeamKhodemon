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
    public partial class Form_Daneshjo_Vorood : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        Form_Daneshjo_EtelaatDars F_Etelaat = new Form_Daneshjo_EtelaatDars();
        Form_Daneshjo_EntekhabVahed F_EntekhabVahed = new Form_Daneshjo_EntekhabVahed();
        Form_Daneshjo_PardakhtShahriye F_Pardakht = new Form_Daneshjo_PardakhtShahriye();
        Form_Daneshjo_Karname F_Karname = new Form_Daneshjo_Karname();

        int Shahriye = 0;
        public Form_Daneshjo_Vorood()
        {
            InitializeComponent();
        }

        private void Form_Daneshjo_Vorod_Load(object sender, EventArgs e)
        {
            this.Left = 210;
            this.Top = 95;


            txtStdFName.Text = Class1.Student_FirstName.ToString();
            txtStdLName.Text = Class1.Student_LastName.ToString();
            txtStdID.Text = Class1.Student_Code.ToString();
            txtStdNationalCode.Text = Class1.StudentNationalCode.ToString();
            txtStdFather.Text = Class1.Student_FathersName.ToString();
            txtStdTavalod.Text = Class1.Student_DateOfBirth.ToString();
            txtStdReshteh.Text = Class1.Student_Reshteh.ToString();
            txtStdVorood.Text = Class1.Student_EnterDate.ToString();
            txtStdMaghta.Text = Class1.StudentMaghta.ToString();
            txtStdTerm.Text = Class1.Student_Term.ToString();

            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();

            Adapter = new SqlDataAdapter("select Shahriye from Table_Student_Info where StudentId like '" + Class1.Student_Code + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            Shahriye = Convert.ToInt32(Dt.Rows[0][0]);

        }

        private void btnEntekhabVahed_Click(object sender, EventArgs e)
        {
            if (Shahriye >= 18000000)
            {
                F_EntekhabVahed.Visible = true;
                if (F_Etelaat.Visible == true)
                    F_Etelaat.Visible = false;
                if (F_Pardakht.Visible == true)
                    F_Pardakht.Visible = false;
                if (F_Karname.Visible == true)
                    F_Karname.Visible = false;
            }
            else
            {
                DialogResult dr = MessageBox.Show("دانشجوی محترم , به علت بدهی قادر به انتخاب واحد در این زمان نیستید  \n حداقل پیش پرداخت شهریه 18.000.000 میلیون ریال است",
                                               "خطا !!!",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error);
            }

        }

        private void btnEtelaatDars_Click(object sender, EventArgs e)
        {
            if (Shahriye >= 18000000)
            {
                F_Etelaat.Visible = true;
                if (F_Pardakht.Visible == true)
                    F_Pardakht.Visible = false;
                if (F_EntekhabVahed.Visible == true)
                    F_EntekhabVahed.Visible = false;
                if (F_Karname.Visible == true)
                    F_Karname.Visible = false;
            }
            else
            {
                DialogResult dr = MessageBox.Show("دانشجوی محترم , به علت بدهی  نمی توانید وارد\n صفحه ..وضعیت دروس دانشجو.. شوید  \n حداقل پیش پرداخت شهریه 18.000.000 میلیون ریال است",
                                               "خطا !!!",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error);
            }

        }

        private void btnPardakhtShahriye_Click(object sender, EventArgs e)
        {
            F_Pardakht.Visible = true;
            if (F_Etelaat.Visible == true)
                F_Etelaat.Visible = false;
            if (F_EntekhabVahed.Visible == true)
                F_EntekhabVahed.Visible = false;
            if (F_Karname.Visible == true)
                F_Karname.Visible = false;
        }

        private void btnKarname_Click(object sender, EventArgs e)
        {
            if (Shahriye >= 18000000)
            {
                F_Karname.Visible = true;
                if (F_Etelaat.Visible == true)
                    F_Etelaat.Visible = false;
                if (F_EntekhabVahed.Visible == true)
                    F_EntekhabVahed.Visible = false;
                if (F_Pardakht.Visible == true)
                    F_Pardakht.Visible = false;
            }
            else
            {
                DialogResult dr = MessageBox.Show("دانشجوی محترم , به علت بدهی   \nنمی توانید کارنامه خود را مشاهده کنید  \n حداقل پیش پرداخت شهریه 18.000.000 میلیون ریال است",
                                               "خطا !!!",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error);
            }


        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا از خروج اطمینان دارید ؟؟",
                                               "اطلاعیه !!!",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                this.Close();
                new Form1().Show();
            }
        }


        private void btnAkhtar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
