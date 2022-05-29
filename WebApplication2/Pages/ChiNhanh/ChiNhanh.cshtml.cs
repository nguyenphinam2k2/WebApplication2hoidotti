using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.ChiNhanh
{
    public class ChiNhanhModel : PageModel
    {
        public List<ChiNhanhInfo> listChiNhanh = new List<ChiNhanhInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=ADMIN-PC;Initial Catalog=GIAOHANG;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ChiNhanh";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChiNhanhInfo chiNhanh = new ChiNhanhInfo();
                                chiNhanh.MaCN = reader.GetString(0);
                                chiNhanh.TenCN = reader.GetString(1);
                                chiNhanh.DiaChi = reader.GetString(2);

                                listChiNhanh.Add(chiNhanh);
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }

        }
    }


    public class ChiNhanhInfo
    {
        public string MaCN = "";
        public string TenCN = "";
        public string DiaChi = "";
    }
}
