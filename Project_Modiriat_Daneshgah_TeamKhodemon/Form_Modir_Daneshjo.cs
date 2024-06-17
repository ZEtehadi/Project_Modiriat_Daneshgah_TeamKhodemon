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
    public partial class Form_Modir_Daneshjo : Form
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
        int StudentId_Select;
        string Student_LName_Select;

        public SqlDataAdapter Adapter_Delete;
        public BindingSource Bin_Delete;
        public DataTable Dt_Delete;
        public Form_Modir_Daneshjo()
        {
            InitializeComponent();
        }

        private DataRow GetRow()//این تابع درست کار نمیکند
        {
            if (Bin.Current != null)
            {
               // MessageBox.Show(dataGridViewX1.SelectedRows.Count.ToString());//درست کار نمیکنه
                 return ((DataRowView)Bin.Current).Row;
            }
            else
                return null;
        }

        private void bntBack_Click(object sender, EventArgs e)
        {
            //new Form_Modir_Vorod().Show();
            //this.Visible = false;

            this.Close();
        }

        public void Form_Modir_Daneshjo_Load(object sender, EventArgs e)
        {
            this.Left = 207;
            this.Top = 126;


            Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
            //. or Z_E\\MSSQLSERVER_2022
            Con.Open();
            Adapter = new SqlDataAdapter("select StudentId,FirstName,Password,LastName,NationalCode,Reshte,Maghta,Shahriye,NumberTerm from Table_Student_Info ", Con);
            Dt = new DataTable();
            Adapter.Fill(Dt);
            Bin = new BindingSource();
            Bin.DataSource = Dt;
            dataGridViewX1.DataSource = Dt;

            dataGridViewX1.AllowUserToAddRows = false;
        }


        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowindex = e.RowIndex;

            Row = dataGridViewX1.Rows[rowindex];
            Student_LName_Select = Row.Cells[3].Value.ToString();//طبق کوِئری اندیس باید بدیم
            StudentId_Select = Convert.ToInt32(Row.Cells[0].Value);

            Class1.IndexOfStudentSelect = rowindex;

            Class1.Student_Id = Convert.ToDecimal(Row.Cells[0].Value);
           Class1.Student_FName = Row.Cells[1].Value.ToString();
            Class1.Student_LName = Row.Cells[3].Value.ToString();
            Class1.Student_Password = Convert.ToDecimal(Row.Cells[2].Value);

            //Class1.Student_FatherName = Row.Cells[3].Value.ToString();
            //Class1.Student_NationalCode = Convert.ToDecimal(Row.Cells[3].Value);
            //Class1.Student_BirtDate = Row.Cells[3].Value.ToString();
            //Class1.Student_Reshte = Row.Cells[3].Value.ToString();
            //Class1.Student_Maghta = Row.Cells[3].Value.ToString();
            //Class1.Student_Shahriye = Convert.ToInt32(Row.Cells[3].Value);
            //Class1.Student_Numberterm = Convert.ToInt32(Row.Cells[3].Value);
            //Class1.Student_Address = Row.Cells[3].Value.ToString();
            //Class1.Student_Phone = Convert.ToDecimal(Row.Cells[3].Value);

            MessageBox.Show(rowindex.ToString());
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AdapterSearch = new SqlDataAdapter("select StudentId,FirstName,Password,LastName,NationalCode,Reshte,Maghta,Shahriye,NumberTerm from Table_Student_Info where StudentId like '%" + txtSearch.Text + "%' or FirstName like N'%" + txtSearch.Text + "%' or LastName like N'%" + txtSearch.Text + "%' or Reshte like N'%" + txtSearch.Text + "%'", Con);
            DtSearch = new DataTable();
            AdapterSearch.Fill(DtSearch);
            BinSearch = new BindingSource();
            BinSearch.DataSource = DtSearch;

            dataGridViewX1.DataSource = BinSearch;
        }


        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {


            DialogResult dr = MessageBox.Show("آیا از حذف دانشجو '" + Student_LName_Select + "'مطمعن هستید؟؟ ",
                                "توجه توجه !!!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Adapter_Delete = new SqlDataAdapter("delete from Table_Student_Info where StudentId='"+StudentId_Select+"' delete from Table_Student_Lesson where StudentCode='"+StudentId_Select+"'", Con);
                Dt_Delete = new DataTable();
                Adapter_Delete.Fill(Dt_Delete);
                Bin_Delete = new BindingSource();
                Bin_Delete.DataSource = Dt_Delete;

                Form_Modir_Daneshjo_Load( sender,  e);
                //dataGridViewX1.DataSource = Dt_Delete;
            }

        }

        private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            new Form_Modir_Daneshjo_AddNewStudent().Show();

        }

        private void btnVirayeshStudent_Click(object sender, EventArgs e)
        {
           
                new Form_Modir_Daneshjo_Virayesh().Show();
        }

        private void btnMoshahedeDars_Click(object sender, EventArgs e)
        {

            new Form_Modir_Daneshjo_MashahedeDars().Show();
        }

      
    }

}



//     private void btnDeleteStudent_Click(object sender, EventArgs e)
//{

//  DataRow DeleteRow = GetRow();

// //int index = dataGridViewX1.GetCellCount(DataGridViewElementStates.Selected);
//// DataRow Row = Dt.Rows[index];//درست کار نمیکنه

// if (DeleteRow == null)
// {
//     MessageBox.Show("دانشجویی که قصد حذف آن را دارین انتخاب کنید",
//                     "توجه توجه !!!",
//                     MessageBoxButtons.OK,
//                     MessageBoxIcon.Warning);
// }
// else
// {
//     string LName = DeleteRow["LastName"].ToString();
//     SqlCommandBuilder Bild = new SqlCommandBuilder(Adapter);
//     DialogResult dr = MessageBox.Show("آیا از حذف دانشجو '" + LName + "'مطمعن هستید؟؟ ",
//                     "توجه توجه !!!",
//                     MessageBoxButtons.YesNo,
//                     MessageBoxIcon.Warning);
//     if (dr == DialogResult.Yes)
//     {
//         Adapter = Bild.DataAdapter;
//         // (Dt.Rows[index]).Delete();//درست کار نمیکنه
//         DeleteRow.Delete();
//         Adapter.Update(Dt);
//         Dt.AcceptChanges();
//     }

// }
//}



/////////////////////////////////////////////////////////////////////////////




//private void btnVirayeshStudent_Click(object sender, EventArgs e)
//{
//DataRow Row = GetRow();//سطری که سلکت شده را بر نمیگرداند

////int index = dataGridViewX1.GetCellCount(DataGridViewElementStates.Selected);
////DataRow Row = Dt.Rows[index];//اینم درست کار نمیکنه


//if (Row == null)
//{
//    MessageBox.Show("دانشجویی که قصد ویرایش اطلاعاتش را دارید انتخاب کنید ",
//                    "توجه توجه !!!",
//                    MessageBoxButtons.OK,
//                    MessageBoxIcon.Warning);
//}
//for (int i = 0; i < Dt.Rows.Count; i++)
//{
//    DataRow dt = Dt.Rows[i];
//    if (dt["StudentId"].ToString() == Row["StudentId"].ToString())
//    {
//        Class1.IndexOfStudentSelect = i;
//        Class1.StudentSelectFName = Row["FirstName"].ToString();
//        Class1.StudentSelectLName = Row["LastName"].ToString();
//        Class1.StudentSelectCode = Convert.ToDecimal(Row["StudentId"]);
//        Class1.StudentSelectPassword = Convert.ToDecimal(Row["Password"]);

//    }
//}

//}


///////////////////////////////////////////////////////////////////////





//DataRow Row = GetRow();

//if (Row == null)
//{
//    MessageBox.Show("دانشجویی که قصد ویرایش اطلاعاتش را دارید انتخاب کنید ",
//                    "توجه توجه !!!",
//                    MessageBoxButtons.OK,
//                    MessageBoxIcon.Warning);
//}
//for (int i = 0; i < Dt.Rows.Count; i++)
//{
//    DataRow dt = Dt.Rows[i];
//    if (dt["StudentId"].ToString() == Row["StudentId"].ToString())
//    {
//        Class1.IndexOfStudentSelect = i;
//        Class1.StudentSelectFName = Row["FirstName"].ToString();
//        Class1.StudentSelectLName = Row["LastName"].ToString();
//        Class1.StudentSelectCode= Convert.ToDecimal(Row["StudentId"]);
//        Class1.StudentSelectPassword=Convert.ToDecimal(Row["Password"]);

//    }
//}
