using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CRUD_Razor.Pages.Computers
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string cs = _configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=computers.db";

            using var con = new SqliteConnection(cs);
            con.Open();

            string query = @"
                UPDATE Computers SET
                    Brand = @Brand,
                    Model = @Model,
                    Processor = @Processor,
                    Ram = @Ram,
                    Storage = @Storage,
                    Price = @Price,
                    Status = @Status
                WHERE Id = @Id";

            using var cmd = new SqliteCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", Computer.Id);
            cmd.Parameters.AddWithValue("@Brand", Computer.Brand ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Model", Computer.Model ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Processor", Computer.Processor ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Ram", Computer.Ram ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Storage", Computer.Storage ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", Computer.Price.HasValue ? (object)Computer.Price.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", Computer.Status ?? (object)DBNull.Value);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
