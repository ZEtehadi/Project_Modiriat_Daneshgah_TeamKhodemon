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
    public partial class Form_Modir_Dars : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter AdapterSearch;
        public BindingSource BinSearch;
        public DataTable DtSearch;

        public SqlDataAdapter Adapter_Delete_SelectLesson;
        public BindingSource Bin_Delete_SelectLesson;
        public DataTable Dt_Delete_SelectLesson;

        int rowindex;
        DataGridViewRow Row;
        int EraeeCode_Select;
        string LessonName_Select;


        public SqlDataAdapter AdapterDelete;
        public BindingSource BinDelete;
        public DataTable DtDelete;

        public decimal ModirCode = Class1.Modir_Code;
        public Form_Modir_Dars()
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

        private void btnBack_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        public void Form_Modir_Dars_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,TeacherCode,TeacherName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime,TakenForTeacher from Table_Admin_Lesson Where AdminCode='" + ModirCode + "' ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Dt;



            dataGridViewX1.AllowUserToAddRows = false;
        }


        private void btnDars_Virayesh_Click(object sender, EventArgs e)
        {
            new Form_Modir_Dars_Virayesh().ShowDialog();
        }

        private void btnDars_AddNew_Click(object sender, EventArgs e)
        {
            new Form_Modir_Dars_NewDars().ShowDialog();

        }

        private void btnDars_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا از حذف درس " + LessonName_Select + " مطمعن هستید؟؟؟",
                               "توجه توجه !!!",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning);
            if (DialogResult.Yes == dr)
            {
                Adapter_Delete_SelectLesson = new SqlDataAdapter("delete from Table_Admin_Lesson where EraeeCode='" + EraeeCode_Select + "'", Con);
                Dt_Delete_SelectLesson = new DataTable();
                Adapter_Delete_SelectLesson.Fill(Dt_Delete_SelectLesson);
                Bin_Delete_SelectLesson = new BindingSource();
                Bin_Delete_SelectLesson.DataSource = Dt_Delete_SelectLesson;


                Form_Modir_Dars_Load(sender, e);

                AdapterDelete = new SqlDataAdapter("delete from Table_Teacher_Lesson where EraeeCode='" + EraeeCode_Select + "' delete from Table_Student_Lesson where EraeeCode='" + EraeeCode_Select + "'", Con);
                DtDelete = new DataTable();
                AdapterDelete.Fill(DtDelete);
                BinDelete = new BindingSource();
                BinDelete.DataSource = DtDelete;
            }




        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AdapterSearch = new SqlDataAdapter("select LessonName, LessonCode, EraeeCode, TeacherCode, TeacherName, NumberVahed, LessonTime, LessonDay, ExamDate, ExamTime, TakenForTeacher from Table_Admin_Lesson where AdminCode='" + ModirCode + "' and( LessonCode Like '%" + txtSearch.Text + "%' or EraeeCode like'%" + txtSearch.Text + "%' or TeacherCode like'%" + txtSearch.Text + "%'or TeacherName like N'%" + txtSearch.Text + "%' or LessonName like N'%" + txtSearch.Text + "%')", Con);
            DtSearch = new DataTable();
            AdapterSearch.Fill(DtSearch);
            BinSearch = new BindingSource();
            BinSearch.DataSource = DtSearch;

            dataGridViewX1.DataSource = BinSearch;

        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowindex = e.RowIndex;

                Row = dataGridViewX1.Rows[rowindex];
                LessonName_Select = Row.Cells[0].Value.ToString();//طبق کوِئری اندیس باید بدیم

                EraeeCode_Select = Convert.ToInt32(Row.Cells[2].Value);

                Class1.LessonName = LessonName_Select.ToString();
                Class1.LessonCode = Convert.ToInt32(Row.Cells[1].Value);
                Class1.EraeeCode = EraeeCode_Select;
                Class1.TeacherCode = Convert.ToDecimal(Row.Cells[3].Value);
                Class1.TeacherName = Row.Cells[4].Value.ToString();
                Class1.NumberVahed = Convert.ToInt32(Row.Cells[5].Value);
                Class1.LessonTime = Row.Cells[6].Value.ToString();
                Class1.LessonDay = Row.Cells[7].Value.ToString();
                Class1.ExamDate = Row.Cells[8].Value.ToString();
                Class1.ExamTime = Row.Cells[9].Value.ToString();
                Class1.TakenForTeacher = Convert.ToInt32(Row.Cells[10].Value);
                Class1.RowIndex_Lesson = Convert.ToInt32(rowindex);

            }

            catch
            {
                MessageBox.Show("اگر قصد انتخاب استادی را دارید، بر روی یکی از مشخصات استاد مورد نظر کلیک کنید.",
                    "توجه توجه !!!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
            }
          
            MessageBox.Show(rowindex.ToString());
        }
    }
}




//private void btnDars_Delete_Click(object sender, EventArgs e)
//{

////////////////////////////////////////////////////////////////////////////

//DataRow RowForDelete = GetRow();

//    string LessonName = RowForDelete["LessonName"].ToString();

//    SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);

//   DialogResult dr= MessageBox.Show("آیا از حذف درس "+LessonName+" مطمعن هستید؟؟؟",
//                    "توجه توجه !!!",
//                    MessageBoxButtons.YesNo,
//                    MessageBoxIcon.Warning);
//    if (DialogResult.Yes == dr)
//    {
//        Adapter = Bild.DataAdapter;
//        RowForDelete.Delete();
//        Adapter.Update(Dt);
//        Dt.AcceptChanges();
//    }

//////////////////////////////////////////////////////////////////////////////////////////////////////

//DataRow DeleteRow = GetRow();
//int EraeeCode = Convert.ToInt32(DeleteRow["EraeeCode"]);
//string LessonName = DeleteRow["LessonName"].ToString();

//SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
//DialogResult dr = MessageBox.Show(" آیا از حذف درس " + LessonName + " مطمعن هستید؟؟؟",
//                                 "توجه توجه!!!",
//                                 MessageBoxButtons.YesNo,
//                                 MessageBoxIcon.Information);
//if (dr == DialogResult.Yes)
//{
//    Adapter = Bild.DataAdapter;
//    DeleteRow.Delete();
//    Adapter.Update(Dt);
//    Dt.AcceptChanges();

//    AdapterDelete = new SqlDataAdapter("delete from Table_Teacher_Lesson where EraeeCode='" + EraeeCode + "' delete from Table_Student_Lesson where EraeeCode='" + EraeeCode + "'", Con);
//    DtDelete = new DataTable();
//    AdapterDelete.Fill(DtDelete);
//    BinDelete = new BindingSource();
//    BinDelete.DataSource = DtDelete;


//}
//}