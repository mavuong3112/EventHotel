using quanlyeventskhachsan.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyeventskhachsan.DAO
{
    internal class Connect : frmHome
    {
        private SqlConnection conn;

        public void connect()
        {
            //Copy data source vao
            string connstring = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=quanlyeventskhachsan;Integrated Security=True";
            try
            {
                conn = new SqlConnection(connstring);
                conn.Open();
                MessageBox.Show("Connection Successful!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed");
            }
        }

        public void disconnect()   // gọi hàm này sau khi đã dùng xong csdl 
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }

    }
}
