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
   
    public partial class Form_Daneshjo_EtelaatDars : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter Adapter_Search;
        public BindingSource Bin_Search;
        public DataTable Dt_Search;

        decimal StudentCode = Class1.Student_Code;
        public Form_Daneshjo_EtelaatDars()
        {
            InitializeComponent();
        }

        private void Form_Daneshjo_EtelaatDars_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;



            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();

            Adapter = new SqlDataAdapter("select LessonCode,LessonName,TeacherName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Student_Lesson where StudentCode='"+StudentCode+"'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;
            Con.Close();

        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            new Form_Daneshjo_Vorod_Front().Show();
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Adapter_Search = new SqlDataAdapter("select LessonCode,LessonName,TeacherName,NumberVahed,LessonTime,LessonDay,ExamDate,ExamTime from Table_Student_Lesson where StudentCode='" + StudentCode + "' and( LessonName like N'%" + txtSearch.Text + "%' or LessonCode like '%" + txtSearch.Text + "%' or EraeeCode like '%" + txtSearch.Text + "%' or LessonDay like N'%" + txtSearch.Text + "%' )", Con);
            Dt_Search = new DataTable();
            Adapter_Search.Fill(Dt_Search);
            Bin_Search = new BindingSource();
            Bin_Search.DataSource = Dt_Search;
            dataGridViewX1.DataSource = Bin_Search;
        }
    }
}
