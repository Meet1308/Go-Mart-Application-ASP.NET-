using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    class DBConnect
    {
        private SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-94H75SOP\SQLEXPRESS;Initial Catalog=Go_MartDB;Integrated Security=True");
        public SqlConnection GetCon()
        {
            return con;
        }    
        public void OpenCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
