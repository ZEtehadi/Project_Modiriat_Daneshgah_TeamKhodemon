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
    public partial class Form_Modir_Dars_NewDars : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public Form_Modir_Dars_NewDars()
        {
            InitializeComponent();
        }

        private void Form_Dars_Virayesh_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;

            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or .
            Con.Open();
            Adapter = new SqlDataAdapter("select * from Table_Admin_Lesson", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
        }
        private void btnSaveLesson_Click(object sender, EventArgs e)
        {

            if (txtLessonName.Text.Trim() == "" || txtLessonCode.Text.Trim() == "" ||
            txtEraeeCode.Text.Trim() == "" || txtTeacherCode.Text.Trim() == "" ||
            txtTeacherName.Text.Trim() == "" || txtNumberVahed.Text.Trim() == "" ||
            txtLessonTime.SelectedIndex < 0 || txtLessonDay.SelectedIndex < 0 ||
            txtExamDate.SelectedIndex < 0 || txtExamTime.SelectedIndex < 0)
            {
                MessageBox.Show("مقادیر خالی را پر کنید !!!",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                if (txtLessonName.Text.Trim() == "")
                    lblLessonName.Text = "*****";
                else
                    lblLessonName.Text = "";

                if (txtLessonCode.Text.Trim() == "")
                    lblLessonCode.Text = "*****";
                else
                    lblLessonCode.Text = "";

                if (txtEraeeCode.Text.Trim() == "")
                    lblEraeeCode.Text = "*****";
                else
                {
                    lblEraeeCode.Text = "";
                }

                if (txtTeacherCode.Text.Trim() == "")
                    lblTeacherCode.Text = "*****";
                else
                    lblTeacherCode.Text = "";

                if (txtTeacherName.Text.Trim() == "")
                    lblTeacherName.Text = "*****";
                else
                    lblTeacherName.Text = "";

                if (txtNumberVahed.Text.Trim() == "")
                    lblNumberVahed.Text = "*****";
                else
                    lblNumberVahed.Text = "";

                if (txtLessonTime.SelectedIndex < 0)
                    lblLessonTime.Text = "*****";
                else
                    lblLessonTime.Text = "";

                if (txtLessonDay.SelectedIndex < 0)
                    lblLessonDay.Text = "*****";
                else
                    lblLessonDay.Text = "";

                if (txtExamDate.SelectedIndex < 0)
                    lblExamDate.Text = "*****";
                else
                    lblExamDate.Text = "";

                if (txtExamTime.SelectedIndex < 0)
                    lblExamTime.Text = "*****";
                else
                    lblExamTime.Text = "";
            }
            else
            {

                try
                {
                    DataRow NewRow = Dt.NewRow();
                    NewRow["AdminCode"] = Class1.Modir_Code;
                    NewRow["LessonName"] = txtLessonName.Text;
                    NewRow["LessonCode"] = txtLessonCode.Text;
                    NewRow["EraeeCode"] = Convert.ToInt32(txtEraeeCode.Text);
                    NewRow["TeacherCode"] = txtTeacherCode.Text;
                    NewRow["TeacherName"] = txtTeacherName.Text;
                    NewRow["NumberVahed"] = txtNumberVahed.Text;
                    NewRow["LessonTime"] = txtLessonTime.Text;
                    NewRow["LessonDay"] = txtLessonDay.Text;
                    NewRow["ExamDate"] = txtExamDate.Text;
                    NewRow["ExamTime"] = txtExamTime.Text;
                    NewRow["TakenForTeacher"] = 0;

                    Dt.Rows.Add(NewRow);
                    SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
                    Adapter.Update(Dt);
                    Dt.AcceptChanges();

                   DialogResult dr= MessageBox.Show("درس با موفقیت ثبت شد ",
                                     "اطلاعیه !!!!",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        txtLessonName.Clear();
                        txtLessonCode.Clear();
                        txtEraeeCode.Clear();
                        txtTeacherCode.Clear();
                        txtTeacherName.Clear();
                        txtNumberVahed.Clear();
                        txtLessonTime.SelectedIndex = -1;
                        txtLessonDay.SelectedIndex = -1;
                        txtExamDate.SelectedIndex = -1;
                        txtExamTime.SelectedIndex = -1;
                    }
                }
                catch
                {
                    MessageBox.Show("کد < ارائه درس > وارد شده تکراری است !!!",
                                                            "اخطار !!!!",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Warning);
                    txtEraeeCode.Clear();



                }

            }

        }

        private void txtLessonName_TextChanged(object sender, EventArgs e)
        {
            if (txtLessonName.Text.Trim() == "")
            {
                lblLessonName.Text = "********";
            }
            else
            {
                lblLessonName.Text = "";
            }
        }

        private void txtLessonCode_TextChanged(object sender, EventArgs e)
        {
            if (txtLessonCode.Text.Trim() == "")
            {
                lblLessonCode.Text = "********";
            }
            else
            {
                lblLessonCode.Text = "";
            }
        }

        private void txtEraeeCode_TextChanged(object sender, EventArgs e)
        {
            //if (txtEraeeCode.Text.Trim() == "")
            //{
            //    lblEraeeCode.Text = "********";
            //}
            //else
            //{
            //    lblEraeeCode.Text = "";
            //}
        }

        private void txtTeacherCode_TextChanged(object sender, EventArgs e)
        {
            if (txtTeacherCode.Text.Trim() == "")
            {
                lblTeacherCode.Text = "********";
            }
            else
            {
                lblTeacherCode.Text = "";
            }
        }

        private void txtTeacherName_TextChanged(object sender, EventArgs e)
        {
            if (txtTeacherName.Text.Trim() == "")
            {
                lblTeacherName.Text = "********";
            }
            else
            {
                lblTeacherName.Text = "";
            }
        }

        private void txtNumberVahed_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberVahed.Text.Trim() == "")
            {
                lblNumberVahed.Text = "********";
            }
            else
            {
                lblNumberVahed.Text = "";
            }
        }

        private void txtLessonTime_TextChanged(object sender, EventArgs e)
        {
            if (txtLessonTime.Text.Trim() == "")
            {
                lblLessonTime.Text = "********";
            }
            else
            {
                lblLessonTime.Text = "";
            }
        }

        private void txtLessonDay_TextChanged(object sender, EventArgs e)
        {
            if (txtLessonDay.Text.Trim() == "")
            {
                lblLessonDay.Text = "********";
            }
            else
            {
                lblLessonDay.Text = "";
            }
        }

        private void txtExamDate_TextChanged(object sender, EventArgs e)
        {
            if (txtExamDate.Text.Trim() == "")
            {
                lblExamDate.Text = "********";
            }
            else
            {
                lblExamDate.Text = "";
            }
        }

        private void txtExamTime_TextChanged(object sender, EventArgs e)
        {
            if (txtExamTime.Text.Trim() == "")
            {
                lblExamTime.Text = "********";
            }
            else
            {
                lblExamTime.Text = "";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Modir_Dars().Show();

            this.Close();

        }
    }
}
