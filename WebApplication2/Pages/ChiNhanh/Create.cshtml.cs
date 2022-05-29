using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.ChiNhanh
{
    public class CreateModel : PageModel
    {
        public ChiNhanhInfo chiNhanh = new ChiNhanhInfo();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            chiNhanh.MaCN = Request.Form["MaCN"];
            chiNhanh.TenCN = Request.Form["TenCN"];
            chiNhanh.DiaChi = Request.Form["DiaChi"];

            if (chiNhanh.MaCN.Length == 0 || chiNhanh.TenCN.Length == 0 || chiNhanh.DiaChi.Length == 0)
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
                    string sql = "INSERT INTO ChiNhanh VALUES" + "(@MaCN, @TenCN, @DiaChi)";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaCN", chiNhanh.MaCN);
                        command.Parameters.AddWithValue("@TenCN", chiNhanh.TenCN);
                        command.Parameters.AddWithValue("@DiaChi", chiNhanh.DiaChi);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return; 
            }

            chiNhanh.TenCN = ""; chiNhanh.MaCN = ""; chiNhanh.DiaChi = "";
            successMessage = " Thêm chi nhánh thành công";
            Response.Redirect("/ChiNhanh/ChiNhanh");
        }
    }
}
