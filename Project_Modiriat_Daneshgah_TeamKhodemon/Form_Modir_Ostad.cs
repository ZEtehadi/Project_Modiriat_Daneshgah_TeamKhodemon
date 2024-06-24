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
    public partial class Form_Modir_Ostad : Form
    {
        public SqlConnection Con;
        public SqlDataAdapter Adapter;
        public BindingSource Bin;
        public DataTable Dt;

        public SqlDataAdapter AdapterSearch;
        public BindingSource BinSearch;
        public DataTable DtSearch;

        public SqlDataAdapter Adapter_Ostad_Lesson_Delete;
        public BindingSource Bin_Ostad_Lesson_Delete;
        public DataTable Dt_Ostad_Lesson_Delete;

        public int IndexOfTeacherSelect;
        public Form_Modir_Ostad()
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

        public void Form_Modir_Ostad_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=.;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //.  OR .
            Con.Open();
            Adapter = new SqlDataAdapter("select TeacherCode,FirstName,LastName,FatherName,NationalCode,Madrak,Phone,Password from Table_Teacher_Info ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Bin;
            Con.Close();

            dataGridViewX1.AllowUserToAddRows = false;
        }

        private void bntBack_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Vorod();
            //this.Hide();

            this.Close();
            new Form_Modir_Vorod_Front().Show();
        }

       

        private void btnVirayesh_Click(object sender, EventArgs e)
        {
            DataRow Row = GetRow();
            
            if (Row == null)
            {
                MessageBox.Show("استادی که قصد ویرایش اطلاعاتش را دارید انتخاب کنید ",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow dt = Dt.Rows[i];
                if (dt["TeacherCode"].ToString() == Row["TeacherCode"].ToString())
                {
                    Class1.IndexOfTeacherSelect = i;

                }
            }

            //new Form_Modir_Ostad_Virayesh().ShowDialog();
            new Form_Modir_Ostad_Virayesh().Visible = true;
            
        }

        private void btnMoshahedeDars_Click(object sender, EventArgs e)
        {
            DataRow Row = GetRow();

            if (Row == null)
            {
                MessageBox.Show("استادی که قصد ویرایش اطلاعاتش را دارید انتخاب کنید ",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow dt = Dt.Rows[i];
                if (dt["TeacherCode"].ToString() == Row["TeacherCode"].ToString())
                {
                    Class1.IndexOfTeacherSelect = i;
                    Class1.OstadSelectFName = Row["FirstName"].ToString();
                    Class1.OstadSelectLName = Row["LastName"].ToString();
                    Class1.OstadSelectCode = Convert.ToDecimal(Row["TeacherCode"]);
                }
            }


            //new Form_Modir_Ostad_MoshahedeDarsEraeShode().ShowDialog();
            new Form_Modir_Ostad_MoshahedeDarsEraeShode().Visible = true;
        }

        private void btnNewOstad_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Ostad_AddNewOstad().ShowDialog();
            //this.Hide();

            new Form_Modir_Ostad_AddNewOstad().Visible = true;
            this.Visible = false;
        }

        
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Con.Open();
            AdapterSearch=new SqlDataAdapter("select TeacherCode, FirstName, LastName, FatherName, NationalCode, Madrak, Address, Phone, Password from Table_Teacher_Info where TeacherCode like '%"+txtSearch.Text+"%' or FirstName like N'%"+txtSearch.Text+"%' or LastName like N'%"+txtSearch.Text+"%' or Madrak like N'%"+txtSearch.Text+"%'", Con);
            DtSearch = new DataTable();
            AdapterSearch.Fill(DtSearch);
            BinSearch = new BindingSource();
            BinSearch.DataSource = DtSearch;

            dataGridViewX1.DataSource = BinSearch;
            Con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow DeleteRow = GetRow();
            if (DeleteRow == null)
            {
                MessageBox.Show("استادی که قصد حذف آن را دارین انتخاب کنید",
                                "توجه توجه !!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                string LName = DeleteRow["LastName"].ToString();
                decimal OstadCodeForDelete=Convert.ToDecimal(DeleteRow["TeacherCode"]);

                SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
                DialogResult dr = MessageBox.Show("آیا از حذف استاد'"+LName+"'مطمعن هستید؟؟ ",
                                "توجه توجه !!!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Adapter = Bild.DataAdapter;
                    DeleteRow.Delete();
                    Adapter.Update(Dt);
                    Dt.AcceptChanges();


                    Adapter_Ostad_Lesson_Delete = new SqlDataAdapter("delete from Table_Teacher_Lesson where TeacherCode='"+OstadCodeForDelete+"'delete from Table_Teacher_Info where TeacherCode='"+OstadCodeForDelete+"' delete from Table_Student_Lesson where TeacherCode='"+OstadCodeForDelete+"'", Con);
                    Dt_Ostad_Lesson_Delete = new DataTable();
                    Adapter_Ostad_Lesson_Delete.Fill(Dt_Ostad_Lesson_Delete);
                    Bin_Ostad_Lesson_Delete = new BindingSource();
                    Bin_Ostad_Lesson_Delete.DataSource = Dt_Ostad_Lesson_Delete;


                }
            }
        }

        private void btnNewOstad_Click_1(object sender, EventArgs e)
        {
            //new Form_Modir_Ostad_AddNewOstad().Show();
            new Form_Modir_Ostad_AddNewOstad().Visible = true;  
        }
    }
}
