using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace QLBH
{
    class KetNoi
    {
<<<<<<< HEAD
        // Chuỗi kết nối
        private string connectionString;
        private SqlConnection conn;

=======
        string conStr = @"Data Source=UMA\SQLEXPRESS;Initial Catalog=QuanLyHieuSach1;Integrated Security=True";
        SqlConnection conn;
>>>>>>> e7a74af6ed5da090ba69ef5bea7af1291b9a8b08
        public KetNoi()
        {
            // Lấy chuỗi kết nối từ file App.config
            connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            conn = new SqlConnection(connectionString);
        }

        // Hàm lấy dữ liệu từ database và trả về DataSet
        public DataSet LayDuLieu(string truyvan)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(truyvan, conn);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }

        // Hàm thực thi lệnh SQL (INSERT, UPDATE, DELETE)
        public bool ThucThi(string truyvan)
        {
            try
            {
                conn.Open(); // Mở kết nối
                SqlCommand cmd = new SqlCommand(truyvan, conn);
                int r = cmd.ExecuteNonQuery();
                return r > 0; // Trả về true nếu có dòng được thực thi
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực thi SQL: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close(); // Đảm bảo đóng kết nối
            }
        }
    }
}
