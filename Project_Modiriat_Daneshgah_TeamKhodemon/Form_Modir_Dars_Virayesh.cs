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
    public partial class Form_Modir_Dars_Virayesh : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;
        public Form_Modir_Dars_Virayesh()
        {
            InitializeComponent();
        }

        private void Form_Modir_Dars_Virayesh_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();
            Adapter = new SqlDataAdapter("select * from Table_Admin_Lesson ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;

            txtLessonName.Text = Class1.LessonName.ToString();
            txtLessonCode.Text = Class1.LessonCode.ToString();
            txtEraeeCode.Text = Class1.EraeeCode.ToString();
            txtTeacherCode.Text = Class1.TeacherCode.ToString();
            txtTeacherName.Text = Class1.TeacherName.ToString();
            txtNumberVahed.Text = Class1.NumberVahed.ToString();
            txtLessonTime.Text = Class1.LessonTime.ToString();
            txtLessonDay.Text = Class1.LessonDay.ToString();
            txtExamDate.Text = Class1.ExamDate.ToString();
            txtExamTime.Text = Class1.ExamTime.ToString();
            if (Class1.TakenForTeacher == 1)
                check_TakenForteacher.Checked = true;
            else
                check_TakenForteacher.Checked = false;






        }
        private void btnSaveLesson_Click(object sender, EventArgs e)
        {

            Dt.Rows[Class1.RowIndex_Lesson][0]=Convert.ToDecimal(Class1.Modir_Code) ;
            Dt.Rows[Class1.RowIndex_Lesson][1] = txtLessonName.Text;
            Dt.Rows[Class1.RowIndex_Lesson][2] = Convert.ToInt32(txtLessonCode.Text);
            Dt.Rows[Class1.RowIndex_Lesson][3] = Convert.ToInt32(txtEraeeCode.Text);
            Dt.Rows[Class1.RowIndex_Lesson][4] = Convert.ToDecimal(txtTeacherCode.Text);
            Dt.Rows[Class1.RowIndex_Lesson][5] = txtTeacherName.Text;
            Dt.Rows[Class1.RowIndex_Lesson][6] = Convert.ToInt32(txtNumberVahed.Text);
            Dt.Rows[Class1.RowIndex_Lesson][7] = txtLessonTime.Text;
            Dt.Rows[Class1.RowIndex_Lesson][8] = txtLessonDay.Text;
            Dt.Rows[Class1.RowIndex_Lesson][9] = txtExamDate.Text;
            Dt.Rows[Class1.RowIndex_Lesson][10] = txtExamTime.Text;
            if (check_TakenForteacher.Checked == true)
                Dt.Rows[Class1.RowIndex_Lesson][11] = 1;
            else
                Dt.Rows[Class1.RowIndex_Lesson][11] = 0;

            SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
            Adapter.Update(Dt);
            Dt.AcceptChanges();

            MessageBox.Show("تغییرات با موفقیت انجام شد",
                            "اطلاعیه!!!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

           

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Modir_Dars().Form_Modir_Dars_Load(sender, e);
            new Form_Modir_Dars().Show();

            this.Close();
        }


    }
}
