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
    public partial class Form_Ostad_EdaeeDars : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter Adapter_insert;
        public BindingSource Bin_insert;
        public DataTable Dt_insert;

        public SqlDataAdapter Adapter_AddLesson;
        public BindingSource Bin_AddLesson;
        public DataTable Dt_AddLesson;

        public SqlDataAdapter Adapter_Search;
        public BindingSource Bin_Search;
        public DataTable Dt_Search;

        decimal TeacherCode = Convert.ToDecimal(Class1.TeachCode);

        public Form_Ostad_EdaeeDars()
        {
            InitializeComponent();
        }

        private DataRow GetRow()
        {
            if (Bin.Current != null)
                return ((DataRowView)Bin.Current).Row;
            else
                return null;
        }

        private void Form_Ostad_EdaeeDars_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime,TakenForTeacher from Table_Admin_Lesson where TeacherCode='" + TeacherCode + "' ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;
            Con.Close();

            dataGridViewX1.AllowUserToAddRows = false;
        }
        private void btnSabtNahayi_Click(object sender, EventArgs e)
        {
            Con.Close();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow row = Dt.Rows[i];

                int EraeeCode = Convert.ToInt32(row["EraeeCode"]);
                int LessonCode = Convert.ToInt32(row["LessonCode"]);
                string LessonName = row["LessonName"].ToString();
                int NumberVahed = Convert.ToInt32(row["NumberVahed"]);
                string LessonDay = row["LessonDay"].ToString();
                string LessonTime = row["LessonTime"].ToString();
                string ExamDate = row["ExamDate"].ToString();
                string ExamTime = row["ExamTime"].ToString();
                MessageBox.Show(EraeeCode.ToString());
                if (Convert.ToInt32(row["TakenForTeacher"]) == 1)//ok
                {
                    Con.Open();
                    SqlCommand Com1 = new SqlCommand("Update Table_Admin_Lesson set TakenForTeacher=1 where EraeeCode='" + EraeeCode + "'", Con);
                    Com1.ExecuteNonQuery();
                    MessageBox.Show("تیبل ادمین درس 1 شد");
                    Con.Close();

                    //Con.Open();
                    //SqlCommand com2 = new SqlCommand("select * from Table_Teacher_Lesson where EraeeCode='" + Convert.ToInt32(EraeeCode) + "'", Con);
                    //SqlDataReader dr2 = com2.ExecuteReader();//استاد این درسو برداشته یا ن
                    //Con.Close();

                    Con.Open();
                    Adapter_insert = new SqlDataAdapter("select * from Table_Teacher_Lesson where EraeeCode='" + Convert.ToInt32(EraeeCode) + "'", Con);
                    Dt_insert = new DataTable();
                    Adapter_insert.Fill(Dt_insert);
                    Bin_insert = new BindingSource();
                    Bin.DataSource = Dt_insert;
                    Con.Close();

                    if (Dt_insert.Rows.Count==0)//استاد درسو برنداشته پس باید اطلاعات درس وارد تیبل استاد بشه
                    {
                        Con.Open();
                        Adapter_AddLesson = new SqlDataAdapter("insert into Table_Teacher_Lesson(TeacherCode,LessonCode,EraeeCode,LessonName,NumberVahed,LessonDay,LessonTime,ExamDate,ExamTime,Moadel) values('" + Convert.ToDecimal(TeacherCode) + "','" + Convert.ToInt32(LessonCode) + "','" + Convert.ToInt32(EraeeCode) + "',N'" + LessonName.ToString() + "', '" + Convert.ToInt32(NumberVahed) + "',N'" + LessonDay.ToString() + "','" + LessonTime.ToString() + "','" + ExamDate.ToString() + "','" + ExamTime.ToString() + "','" + 0 + "' )", Con);
                        Dt_AddLesson = new DataTable();
                        Adapter_AddLesson.Fill(Dt_AddLesson);
                        Bin_AddLesson = new BindingSource();
                        Bin_AddLesson.DataSource = Dt_AddLesson;
                        Con.Close();
                        MessageBox.Show("اطلاعات درس منتقل شد");
                    }
                    else { }
                }
                else if(Convert.ToInt32(row["TakenForTeacher"]) == 0)
                {

                    Con.Open();
                    SqlCommand Com2 = new SqlCommand("Update Table_Admin_Lesson set TakenForTeacher=0 where EraeeCode='" + EraeeCode + "'", Con);
                    Com2.ExecuteNonQuery();
                    MessageBox.Show("در جدول مدیر 0 شد");
                    Con.Close();

                    Con.Open();
                    SqlCommand Com3 = new SqlCommand("delete Table_Teacher_Lesson where EraeeCode='" + EraeeCode + "'", Con);
                    Com3.ExecuteNonQuery();
                    MessageBox.Show("درس از جدول استاد حذف شد");
                    Con.Close();
                }
            }
            MessageBox.Show("انتخاب دروس با موفقیت انجام شد ",
                     "اطلاعیه !!!",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
            Con.Open();

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Ostad_Vorod_Front().Show();
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            Adapter_Search = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime,TakenForTeacher from Table_Admin_Lesson where TeacherCode='"+TeacherCode+"' and ( LessonName like N'%" + txtSearch.Text + "%' or LessonCode like '%" + txtSearch.Text + "%' or EraeeCode like '%" + txtSearch.Text + "%' or LessonDay like N'%" + txtSearch.Text + "%' )", Con);
            Dt_Search = new DataTable();
            Adapter_Search.Fill(Dt_Search);
            Bin_Search = new BindingSource();
            Bin_Search.DataSource = Dt_Search;
            dataGridViewX1.DataSource = Bin_Search;
            Con.Close();
        }
    }
}
