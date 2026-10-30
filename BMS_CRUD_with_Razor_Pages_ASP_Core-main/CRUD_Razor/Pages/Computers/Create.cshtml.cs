using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CRUD_Razor.Pages.Computers
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Computer Computer { get; set; } = new();

        public void OnGet()
        {
            // Default status
            Computer.Status = "In Stock";
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
                INSERT INTO Computers
                (Brand, Model, Processor, Ram, Storage, Price, Status)
                VALUES
                (@Brand, @Model, @Processor, @Ram, @Storage, @Price, @Status)";

            using var cmd = new SqliteCommand(query, con);
            cmd.Parameters.AddWithValue("@Brand", Computer.Brand ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Model", Computer.Model ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Processor", Computer.Processor ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Ram", Computer.Ram ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Storage", Computer.Storage ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", Computer.Price.HasValue ? (object)Computer.Price.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", Computer.Status ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();

            return RedirectToPage("Index");
        }
    }
}
