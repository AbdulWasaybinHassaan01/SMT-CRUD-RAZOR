using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CRUD_Razor.Pages.Computers
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Computer Computer { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=computers.db";

            using var con = new SqliteConnection(cs);
            con.Open();

            string query = "SELECT Id, Brand, Model, Processor, Ram, Storage, Price, Status FROM Computers WHERE Id = @Id";

            using var cmd = new SqliteCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                return NotFound();
            }

            Computer = new Computer
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Brand = reader["Brand"] as string ?? string.Empty,
                Model = reader["Model"] as string ?? string.Empty,
                Processor = reader["Processor"] as string,
                Ram = reader["Ram"] as string,
                Storage = reader["Storage"] as string,
                Price = reader["Price"] == DBNull.Value ? null : Convert.ToDecimal(reader["Price"]),
                Status = reader["Status"] as string
            };

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=computers.db";

            using var con = new SqliteConnection(cs);
            con.Open();

            string query = "DELETE FROM Computers WHERE Id = @Id";

            using var cmd = new SqliteCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
