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
    public partial class Form_Osatad_BarnameHaftegi : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter Adapter_CopyToTable;
        public BindingSource Bin_CopyToTable;
        public DataTable Dt_CopyToTable;

        public SqlDataAdapter Adapter_Search;
        public BindingSource Bin_Search;
        public DataTable Dt_Search;

        public SqlDataAdapter Adapter_ShowList;
        public BindingSource Bin_ShowList;
        public DataTable Dt_ShowList;

        int rowindex;
        DataGridViewRow Row;
        public string EraeeCode_ShowList;

        public decimal OstadCode = Class1.TeachCode;
        public Form_Osatad_BarnameHaftegi()
        {
            InitializeComponent();
        }

        private void Form_Osatad_BarnameHaftegi_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR .
            Con.Open();

            //try
            //{
            //    Adapter_CopyToTable = new SqlDataAdapter("insert Table_Teacher_Lesson(TeacherCode,LessonCode,EraeeCode,lessonName,NumberVahed,LessonDay,LessonTime,ExamDate,ExamTime) select TeacherCode, LessonCode, EraeeCode, lessonName, NumberVahed, LessonDay, LessonTime, ExamDate, ExamTime from Table_Admin_Lesson where TakenForTeacher = 1 and TeacherCode ='" + OstadCode + "' ", Con);
            //    Dt_CopyToTable = new DataTable();
            //    Adapter_CopyToTable.Fill(Dt_CopyToTable);
            //    Bin_CopyToTable = new BindingSource();
            //    Bin_CopyToTable.DataSource = Dt_CopyToTable;
            //}
            //catch { }

            Adapter = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Admin_Lesson where TakenForTeacher=1 and TeacherCode='" + OstadCode + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;
            Con.Close();
            dataGridViewX1.AllowUserToAddRows = false;
        }
        //Adapter = new SqlDataAdapter("insert Table_Teacher_Lesson(TeacherCode,LessonCode,EraeeCode,lessonName,NumberVahed,LessonDay,LessonTime,ExamDate,ExamTime) select TeacherCode, LessonCode, EraeeCode, lessonName, NumberVahed, LessonDay, LessonTime, ExamDate, ExamTime from Table_Admin_Lesson where TakenForTeacher = 1 and TeacherCode ='"+OstadCode+"' "   , Con);

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Ostad_Vorod().Show();
            this.Hide();
        }


        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            datagrid_ShowStudent.Visible = true;
            btnHideDataGridStudent.Visible = true;

            rowindex = e.RowIndex;
            Row = dataGridViewX1.Rows[rowindex];
            EraeeCode_ShowList = Row.Cells[2].Value.ToString();

            Adapter_ShowList = new SqlDataAdapter("select Table_Student_Info.StudentId,Table_Student_Info.FirstName,Table_Student_Info.LastName,Table_Student_Info.FatherName,Table_Student_Lesson.StudentCode,Table_Student_Lesson.EraeeCode,Table_Student_Lesson.Score from Table_Student_Info inner join Table_Student_Lesson on Table_Student_Info.StudentId = Table_Student_Lesson.StudentCode where Table_Student_Lesson.EraeeCode ='" + EraeeCode_ShowList + "' ", Con);
            Dt_ShowList = new DataTable();
            Adapter_ShowList.Fill(Dt_ShowList);
            Bin_ShowList = new BindingSource();
            Bin_ShowList.DataSource = Dt_ShowList;

            datagrid_ShowStudent.DataSource = Dt_ShowList;
            datagrid_ShowStudent.AllowUserToAddRows = false;

        }


        private void btnHideDataGridStudent_Click(object sender, EventArgs e)
        {
            Con.Close();
            for (int i = 0; i < Dt_ShowList.Rows.Count; i++)
            {
                DataRow dr = Dt_ShowList.Rows[i];
                decimal StId = Convert.ToDecimal(dr["StudentId"]);//ok
                double Score = Convert.ToDouble(dr["Score"]);//ok
                Con.Open();
                SqlCommand com = new SqlCommand("Update Table_Student_lesson set Score='"+Score+"' where StudentCode='" + StId + "' and EraeeCode='"+EraeeCode_ShowList+"'", Con);
                com.ExecuteNonQuery();
                Con.Close();

            }


            datagrid_ShowStudent.Visible = false;
            btnHideDataGridStudent.Visible = false;
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Con.Open();
            Adapter_Search = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime,TakenForTeacher from Table_Admin_Lesson where TakenForTeacher=1 and TeacherCode='" + OstadCode + "'and(LessonName like N'%" + txtSearch.Text + "%' or LessonCode like '%" + txtSearch.Text + "%' or EraeeCode like '%" + txtSearch.Text + "%' or LessonDay like N'%" + txtSearch.Text + "%' )", Con);
            Dt_Search = new DataTable();
            Adapter_Search.Fill(Dt_Search);
            Bin_Search = new BindingSource();
            Bin_Search.DataSource = Dt_Search;
            dataGridViewX1.DataSource = Bin_Search;
            Con.Close();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            new Form_Ostad_Vorod_Front().Show();
            this.Close();
        }

    }
}
