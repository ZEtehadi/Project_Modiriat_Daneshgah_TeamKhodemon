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
    public partial class Form_Modir_Daneshjo_MashahedeDars : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter AdapterSearch;
        public BindingSource BinSearch;
        public DataTable DtSearch;

        int rowindex;
        DataGridViewRow Row;

        string Student_LName_Select;
        public decimal StudentCode = Class1.Student_Id;
        public int EraeeCode_Select;
        public string LessonName_Select;

        public SqlDataAdapter Adapter_Delete;
        public BindingSource Bin_Delete;
        public DataTable Dt_Delete;

        public Form_Modir_Daneshjo_MashahedeDars()
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

        private void Form_Modir_Daneshjo_MashahedeDars_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            txtStudent_Code.Text = Class1.Student_Id.ToString();
            txtStudent_FName.Text = Class1.Student_FName.ToString();
            txtStudent_LName.Text = Class1.Student_LName.ToString();


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select LessonCode,EraeeCode,LessonName,TeacherName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Student_Lesson where StudentCode='" + StudentCode + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Dt;

            //dataGridViewX1.DataSource = Dt;

            dataGridViewX1.AllowUserToAddRows = false;
        }

        private void bntBack_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Daneshjo().Form_Modir_Daneshjo_Load(sender, e);
            new Form_Modir_Daneshjo().Show();

            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("آیا از حذف درس'" + LessonName_Select + "'مطمعن هستید؟؟ ",
                            "توجه توجه !!!",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Adapter_Delete = new SqlDataAdapter("delete From Table_Student_Lesson where EraeeCode='"+EraeeCode_Select+"'", Con);
                Dt_Delete = new DataTable();
                Adapter_Delete.Fill(Dt_Delete);
                Bin_Delete = new BindingSource();
                Bin_Delete.DataSource = Dt_Delete;
            }



            //DataRow DeleteRow = GetRow();
            //if (DeleteRow == null)
            //{
            //    MessageBox.Show("درسی که قصد حذف آن را دارین انتخاب کنید",
            //                    "توجه توجه !!!",
            //                    MessageBoxButtons.OK,
            //                    MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    string LessonName = DeleteRow["LessonName"].ToString();
            //    // int EraeeCode = Convert.ToInt32(DeleteRow["EraeeCode"]);
            //    SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
            //    DialogResult dr = MessageBox.Show("آیا از حذف درس'" + LessonName + "'مطمعن هستید؟؟ ",
            //                    "توجه توجه !!!",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Warning);
            //    if (dr == DialogResult.Yes)
            //    {
            //        Adapter = Bild.DataAdapter;
            //        DeleteRow.Delete();
            //        Adapter.Update(Dt);
            //        Dt.AcceptChanges();

            //    }
            //}
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            AdapterSearch = new SqlDataAdapter("select LessonCode,EraeeCode,LessonName,TeacherName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Student_Lesson where  StudentCode='" + StudentCode + "'and( LessonCode like'%" + txtSearch.Text+"%' or LessonName like N'%"+txtSearch.Text+"%' or EraeeCode like '%"+txtSearch.Text+"%' or LessonDay like N'%"+txtSearch.Text+"%') ", Con);
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
                EraeeCode_Select = Convert.ToInt32(Row.Cells[1].Value);
                LessonName_Select = Row.Cells[2].Value.ToString();
            }
            catch { }
        }

        private void panelEx2_Click(object sender, EventArgs e)
        {

        }

        private void txtStudent_Code_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }
    }
}