using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.ChiNhanh
{
    public class EditModel : PageModel
    {
        public ChiNhanhInfo chiNhanh = new ChiNhanhInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string MaCN = Request.Query["MaCN"];
            try
            {
                string connectionString = "Data Source=ADMIN-PC;Initial Catalog=GIAOHANG;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ChiNhanh WHERE MaCN=@MaCN";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaCN", MaCN);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chiNhanh.MaCN = reader.GetString(0);
                                chiNhanh.TenCN = reader.GetString(1);
                                chiNhanh.DiaChi = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            chiNhanh.MaCN = Request.Form["MaCN"];
            chiNhanh.TenCN = Request.Form["TenCN"];
            chiNhanh.DiaChi = Request.Form["DiaChi"];

            if (chiNhanh.MaCN.Length == 0 || chiNhanh.TenCN.Length == 0 || 
                chiNhanh.DiaChi.Length == 0 )
            {
                errorMessage = "Warning: Thiếu dữ liệu !!!";
                return;
            }
            
            // save new chinhanh into database
            try
            {
                string connectionString = "Data Source=ADMIN-PC;Initial Catalog=GIAOHANG;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE ChiNhanh " +
                                 "SET TenCN=@TenCN, DiaChi=@DiaChi " +
                                 "WHERE MaCN=@MaCN ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TenCN", chiNhanh.TenCN);
                        command.Parameters.AddWithValue("@DiaChi", chiNhanh.DiaChi);
                        command.Parameters.AddWithValue("@MaCN", chiNhanh.MaCN);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/ChiNhanh/ChiNhanh");

        }

    }
}

