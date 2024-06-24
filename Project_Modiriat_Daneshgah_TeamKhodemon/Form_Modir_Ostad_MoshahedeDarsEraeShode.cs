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
    public partial class Form_Modir_Ostad_MoshahedeDarsEraeShode : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter AdapterLesson_Search;
        public BindingSource BinLesson_Search;
        public DataTable DtLesson_Search;

        public decimal TeacherCode = Class1.OstadSelectCode;

        int rowindex;
        DataGridViewRow Row;
        public string EraeeCode_ShowList;

        public SqlDataAdapter Adapter_ShowList;
        public BindingSource Bin_ShowList;
        public DataTable Dt_ShowList;

        int EraeeCode_Delete;
        public Form_Modir_Ostad_MoshahedeDarsEraeShode()
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

        private void Form_Modir_Ostad_MoshahedeDarsEraeShode_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;

            txtOstad_Code.Text = Class1.OstadSelectCode.ToString();
            txtOstad_FName.Text = Class1.OstadSelectFName.ToString();
            txtOstad_LName.Text = Class1.OstadSelectLName.ToString();

            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();
            Adapter = new SqlDataAdapter("select LessonCode,EraeeCode,LessonName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Teacher_Lesson where TeacherCode='"+TeacherCode+"'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;

            dataGridViewX1.DataSource = Dt;

           
           

            dataGridViewX1.AllowUserToAddRows = false;
            datagrid_ShowStudent.AllowUserToAddRows = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Modir_Ostad().Show();
            //this.Hide();

            //Class1.F_Ostad.Form_Modir_Ostad_Load(sender, e);
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AdapterLesson_Search = new SqlDataAdapter("select LessonCode,EraeeCode,LessonName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Teacher_Lesson where TeacherCode='"+TeacherCode+"' and( LessonCode like '%" + txtSearch.Text + "%'or EraeeCode like '%" + txtSearch.Text + "%' or LessonDay like N'%" + txtSearch.Text + "%' or LessonName like N'%"+txtSearch.Text+"%')", Con);
            DtLesson_Search = new DataTable();
            AdapterLesson_Search.Fill(DtLesson_Search);
            BinLesson_Search = new BindingSource();
            BinLesson_Search.DataSource = DtLesson_Search;

            dataGridViewX1.DataSource = DtLesson_Search;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Con.Close();
            //DataRow DeleteRow = GetRow();
            if (rowindex == null)
            {
                MessageBox.Show("استادی که قصد حذف آن را دارین انتخاب کنید",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                //string LessonName = DeleteRow["LessonName"].ToString();
                //int EraeeCode = Convert.ToInt32(DeleteRow["EraeeCode"]);
                //SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
                string LessonName = Row.Cells[2].Value.ToString();
                DialogResult dr = MessageBox.Show("آیا از حذف درس'" + LessonName + "'مطمعن هستید؟؟ ",
                                "توجه توجه !!!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Con.Open();
                    SqlCommand com1 = new SqlCommand("Delete from Table_Teacher_Lesson where EraeeCode='" + EraeeCode_Delete + "'", Con);
                    com1.ExecuteNonQuery();
                    //Adapter = Bild.DataAdapter;
                    //DeleteRow.Delete();//حذف درس از تیبل استاد-درس
                    //Adapter.Update(Dt);
                    //Dt.AcceptChanges();
                    Con.Close();

                    Con.Open();
                    SqlCommand com = new SqlCommand("delete from Table_Student_Lesson where EraeeCode='"+EraeeCode_Delete+"' update Table_Admin_Lesson set TakenForTeacher=0 where EraeeCode='"+EraeeCode_Delete+"' ",Con);
                    com.ExecuteNonQuery();
                    Con.Close();
                    Con.Open();
                    Adapter = new SqlDataAdapter("select LessonCode,EraeeCode,LessonName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Teacher_Lesson where TeacherCode='" + TeacherCode + "'", Con);
                    Dt = new DataTable();
                    Adapter.Fill(Dt);
                    Bin = new BindingSource();
                    Bin.DataSource = Dt;

                    dataGridViewX1.DataSource = Dt;
                    Con.Close();
                    datagrid_ShowStudent.Visible = false;
                    btnHideDataGridStudent.Visible = false;
                }
            }

           
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            datagrid_ShowStudent.Visible = true;
            btnHideDataGridStudent.Visible = true;

            try
            {
                rowindex = e.RowIndex;
                Row = dataGridViewX1.Rows[rowindex];
                EraeeCode_ShowList = Row.Cells[1].Value.ToString();
                EraeeCode_Delete = Convert.ToInt32(Row.Cells[1].Value);
            }
            catch { }
            Adapter_ShowList = new SqlDataAdapter("select Table_Student_Info.StudentId,Table_Student_Info.FirstName,Table_Student_Info.LastName,Table_Student_Info.FatherName,Table_Student_Lesson.StudentCode,Table_Student_Lesson.EraeeCode from Table_Student_Info inner join Table_Student_Lesson on Table_Student_Info.StudentId = Table_Student_Lesson.StudentCode where Table_Student_Lesson.EraeeCode ='"+EraeeCode_ShowList+"' ", Con);
            Dt_ShowList = new DataTable();
            Adapter_ShowList.Fill(Dt_ShowList);
            Bin_ShowList = new BindingSource();
            Bin_ShowList.DataSource = Dt_ShowList;

            datagrid_ShowStudent.DataSource = Dt_ShowList;
        }

        private void btnHideDataGridStudent_Click(object sender, EventArgs e)
        {
            datagrid_ShowStudent.Visible = false;
            btnHideDataGridStudent.Visible = false;
        }
    }
}
