using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Project_Modiriat_Daneshgah_TeamKhodemon
{
    class SqlConnect
    {
        SqlConnection Con = new SqlConnection("Data Source=Z_E\\MSSQLSERVER_2022;Initial Catalog=DBEntekhabVahed_teamKhodemon1;Integrated Security=True");
        //.  OR Z_E\MSSQLSERVER_2022
        SqlDataAdapter Adapter;

        private void Connect()
        {
            Con.Open();
        }
        private void DisConnect()
        {
            Con.Close();
        }
        private void GetQuerry(string x)
        {
            Adapter = new SqlDataAdapter(x, Con);

        }





    }
}
