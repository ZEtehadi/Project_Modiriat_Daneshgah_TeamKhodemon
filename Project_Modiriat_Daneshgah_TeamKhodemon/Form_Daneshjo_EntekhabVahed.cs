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
    public partial class Form_Daneshjo_EntekhabVahed : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter Adapter_F_T;
        public BindingSource Bin_F_T;
        public DataTable Dt_F_T;

        public SqlDataAdapter Adapter_LessonStudent;
        public BindingSource Bin_LessonStudent;
        public DataTable Dt_LessonStudent;

        public SqlDataAdapter Adapter_AllLesson;
        public BindingSource Bin_AllLesson;
        public DataTable Dt_AllLesson;

        int i;
        DataGridViewRow Row;
        public int EraeeCode_Click;

        decimal StudentCode = Class1.Student_Code;
        int LessonCode;
        string LessonName;
        int EraeeCode;
        decimal TeacherCode;
        string TeacherName;
        int NumberVahed;
        //4chis Khalii
        string LessonDay;
        string LessonTime;
        string ExamTime;
        string ExamDate;


        string LessonDay_lastlesson;
        string LessonTime_lastLesson;
        string ExamTime_lestlesson;
        string ExamDate_lastlesson;
        string LessonName_lastlesson;

        string NameLesson;


        public Form_Daneshjo_EntekhabVahed()
        {
            InitializeComponent();
        }

        private void Form_Daneshjo_EntekhabVahed_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;

            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();

            Adapter = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,TeacherName,TeacherCode,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Admin_Lesson where TakenForTeacher=1 ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;

            Adapter_LessonStudent = new SqlDataAdapter("Select * from Table_Student_Lesson where StudentCode='" + StudentCode + "'", Con);
            Dt_LessonStudent = new DataTable();
            Adapter_LessonStudent.Fill(Dt_LessonStudent);
            Bin_LessonStudent = new BindingSource();
            Bin_LessonStudent.DataSource = Dt_LessonStudent;

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow dt = Dt.Rows[i];
                int Code = Convert.ToInt32(dt["EraeeCode"]);
                ; for (int j = 0; j < Dt_LessonStudent.Rows.Count; j++)
                {
                    DataRow row = Dt_LessonStudent.Rows[j];
                    int CodeStudent = Convert.ToInt32(row["EraeeCode"]);
                    if (Code == CodeStudent)
                    {
                        dataGridViewX1.Rows[i].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                        dataGridViewX1.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                }
            }
            Con.Close();
            dataGridViewX1.AllowUserToAddRows = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ///جستجوی درس مورد نظر
            Con.Open();
            Adapter= new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,TeacherName,TeacherCode,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Admin_Lesson where TakenForTeacher=1 and (LessonName like N'%" + txtSearch.Text + "%' or LessonCode like '%" + txtSearch.Text + "%' or EraeeCode like '%" + txtSearch.Text + "%') ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;

            //برای نمایش دادن درسهایی که دانشجو انتخاب کرده به رنگ سبز
            Adapter_LessonStudent = new SqlDataAdapter("Select * from Table_Student_Lesson where StudentCode='" + StudentCode + "'", Con);
            Dt_LessonStudent = new DataTable();
            Adapter_LessonStudent.Fill(Dt_LessonStudent);
            Bin_LessonStudent = new BindingSource();
            Bin_LessonStudent.DataSource = Dt_LessonStudent;

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow dt = Dt.Rows[i];
                int Code = Convert.ToInt32(dt["EraeeCode"]);
                ; for (int j = 0; j < Dt_LessonStudent.Rows.Count; j++)
                {
                    DataRow row = Dt_LessonStudent.Rows[j];
                    int CodeStudent = Convert.ToInt32(row["EraeeCode"]);
                    if (Code == CodeStudent)
                    {
                        dataGridViewX1.Rows[i].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                        dataGridViewX1.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                }
            }

            Con.Close();
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Con.Open();
            try
            {
                i = e.RowIndex;
                Row = dataGridViewX1.Rows[i];
                EraeeCode_Click = Convert.ToInt32(Row.Cells[2].Value);//ok
            }
            catch { }


            //برای پیدا کردن اینکه ایا دانشجو این درس را دارد یا نه؟
            Adapter_F_T = new SqlDataAdapter("select  * from Table_Student_Lesson where EraeeCode='" + EraeeCode_Click + "' and StudentCode='" + StudentCode + "'", Con);
            Dt_F_T = new DataTable();
            Adapter_F_T.Fill(Dt_F_T);
            Bin_F_T = new BindingSource();
            Bin_F_T.DataSource = Dt_F_T;

            //برای پیدا کردن تماتم دروس دانشجو تا بعدا زمان ها و تاریخ هارا مقایسه کینم
            Adapter_AllLesson = new SqlDataAdapter("Select * from Table_Student_Lesson where StudentCode='" + StudentCode + "'", Con);
            Dt_AllLesson = new DataTable();
            Adapter_AllLesson.Fill(Dt_AllLesson);
            Bin_AllLesson = new BindingSource();
            Bin_AllLesson.DataSource = Dt_AllLesson;

            Con.Close();
            try
            {
                if (dataGridViewX1.Rows[i].DefaultCellStyle.ForeColor != Color.Green)
                {
                    if (Dt_F_T.Rows.Count == 0)//dr1 == null
                    {
                        /////////اطلاعات درس انتخاب شده بر ای انتقال به تیبله دانشجو-درس
                        LessonCode = Convert.ToInt32(Row.Cells[1].Value);
                        LessonName = Row.Cells[0].Value.ToString();
                        EraeeCode = Convert.ToInt32(Row.Cells[2].Value);
                        TeacherCode = Convert.ToDecimal(Row.Cells[4].Value);//
                        TeacherName = Row.Cells[3].Value.ToString();
                        NumberVahed = Convert.ToInt32(Row.Cells[5].Value);
                        LessonDay = Row.Cells[7].Value.ToString();
                        LessonTime = Row.Cells[6].Value.ToString();
                        ExamTime = Row.Cells[9].Value.ToString();
                        ExamDate = Row.Cells[8].Value.ToString();


                        int n = 0;
                        ////////////اطلاعات دروس قبلیه دانشجو برای مقایسه زمان و تاریخ
                        for (int k = 0; k < Dt_AllLesson.Rows.Count; k++)
                        {
                            DataRow data = Dt_AllLesson.Rows[k];

                            LessonDay_lastlesson = data["LessonDay"].ToString();
                            LessonTime_lastLesson = data["LessonTime"].ToString();
                            ExamTime_lestlesson = data["ExamTime"].ToString();
                            ExamDate_lastlesson = data["ExamDate"].ToString();
                            LessonName_lastlesson = data["LessonName"].ToString();

                            if ((LessonDay_lastlesson == LessonDay && LessonTime_lastLesson == LessonTime)
                                || (ExamDate_lastlesson == ExamDate && ExamTime_lestlesson == ExamTime)
                                || (LessonName_lastlesson == LessonName))
                            {
                                n++;
                                NameLesson = LessonName_lastlesson;
                            }
                            else{}
                        }

                        if (n==0)
                        {
                            Con.Open();
                            SqlCommand com2 = new SqlCommand("insert into Table_Student_Lesson(StudentCode,LessonCode,LessonName,EraeeCode,TeacherCode,TeacherName,NumberVahed,Score,ScoreWithZarib,Pass,Moadel,LessonTime,LessonDay,ExamDate,ExamTime) values('" + StudentCode + "', '" + LessonCode + "', N'" + LessonName + "','" + EraeeCode + "' ,'" + TeacherCode + "' , N'" + TeacherName + "', '" + NumberVahed + "', NULL, NULL, NULL, NULL, '" + LessonTime + "', N'" + LessonDay + "','" + ExamDate + "' ,'" + ExamTime + "' )", Con);
                            com2.ExecuteNonQuery();
                            Con.Close();

                            dataGridViewX1.Rows[i].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                            dataGridViewX1.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            if (LessonName == NameLesson)
                            {
                                MessageBox.Show(" دانشجوی گرامی ,شما قبلا درس " + LessonName + " را دریافت کردید ",
                                      "!!! خطا در ثبت درس",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show("درس مورد نظر با یکی از دروس تداخل زمانی دارد",
                                                                   "!!! خطا در ثبت درس",
                                                                   MessageBoxButtons.OK,
                                                                   MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else{ }
                }
                else
                {
                    Con.Open();
                    SqlCommand Com2 = new SqlCommand("delete from Table_Student_Lesson where EraeeCode='" + EraeeCode_Click + "' and StudentCode='" + StudentCode + "'", Con);
                    Com2.ExecuteNonQuery();
                    Con.Close();

                    dataGridViewX1.Rows[i].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                    dataGridViewX1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
            catch { }

        }

        private void btnSaveLesson_Click(object sender, EventArgs e)
        {
            MessageBox.Show("عملیات انتخاب واحد با موفقیت ثبت شد",
                            "!!! اطلاعیه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Daneshjo_Vorod_Front().Show();
            this.Close();
        }
    }
}
