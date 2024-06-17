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
    public partial class Form_Daneshjo_Karname : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        decimal StudentCode = Class1.Student_Code;

        int Zarib;
        double Score;
        double ScoreZarib;
        string Pass;
        int Eraeecode;

        public Form_Daneshjo_Karname()
        {
            InitializeComponent();
        }

        private void Form_Daneshjo_Karname_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;

            txtStdFName.Text = Class1.Student_FirstName.ToString();
            txtStdLName.Text = Class1.Student_LastName.ToString();
            txtStdID.Text = Class1.Student_Code.ToString();

            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,TeacherName,NumberVahed,Score,ScoreWithZarib,Pass from Table_Student_Lesson where  StudentCode='" + StudentCode + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;
            Con.Close();

            double Average = 0;
            int ZaribKol = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow dt = Dt.Rows[i];
                Eraeecode = Convert.ToInt32(dt["EraeeCode"]);
                Zarib = Convert.ToInt32(dt["NumberVahed"]);

                try
                {
                    if ((dt["Score"]) == null || Convert.ToDouble(dt["Score"]) >= 0)
                    {
                        Score = Convert.ToDouble(dt["Score"]);
                    }
                    else
                    {
                        Score = 0;
                    }
                    //MessageBox.Show("نمره دانشجو نال نیست");
                }
                catch
                {
                    Con.Close();
                    Con.Open();
                    SqlCommand com = new SqlCommand("Update Table_Student_Lesson set Score=0 where StudentCode='" + StudentCode + "' and EraeeCode='" + Eraeecode + "'", Con);
                    com.ExecuteNonQuery();
                    Con.Close();
                   // MessageBox.Show("نمره دانشجو اپدیت شد");
                }

                if (Score >= 10)
                {
                    ScoreZarib = Zarib * Score;
                    Average += ScoreZarib;//معدل دانشجو
                    ZaribKol += Zarib;
                }
                else
                {
                    ScoreZarib = 0;
                }

                if (Score == null || Score == 0)
                {
                    Pass = "بدون نمره";
                    // Score = 0;
                }
                if (Score >= 10)
                {
                    Pass = "پاس شده";
                }
                if (Score < 10 && Score > 0)
                {
                    Pass = "حذف درس";
                }


                Con.Open();
                SqlCommand mon = new SqlCommand("Update Table_Student_Lesson Set ScoreWithZarib='" + ScoreZarib + "' , Pass=N'" + Pass + "' where StudentCode='" + StudentCode + "' and EraeeCode='" + Eraeecode + "'", Con);
                mon.ExecuteNonQuery();
                Con.Close();
            }

            Con.Open();
            Adapter = new SqlDataAdapter("select LessonName,LessonCode,EraeeCode,TeacherName,NumberVahed,Score,ScoreWithZarib,Pass from Table_Student_Lesson where  StudentCode='" + StudentCode + "'", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;

            txtAverage.Text = (Math.Round((Average / ZaribKol), 2)).ToString();

        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
