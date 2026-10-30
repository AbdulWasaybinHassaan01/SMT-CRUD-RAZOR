using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CRUD_Razor.Pages.Computers
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Computer> Computers { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public void OnGet()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=computers.db";

            using var con = new SqliteConnection(cs);
            con.Open();

            string query = "SELECT Id, Brand, Model, Processor, Ram, Storage, Price, Status FROM Computers ";

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query += "WHERE Brand LIKE @search OR Model LIKE @search OR Processor LIKE @search OR Status LIKE @search ";
            }

            query += "ORDER BY Id DESC";

            using var cmd = new SqliteCommand(query, con);
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                cmd.Parameters.AddWithValue("@search", $"%{SearchTerm.Trim()}%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Computers.Add(new Computer
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Brand = reader["Brand"] as string ?? string.Empty,
                    Model = reader["Model"] as string ?? string.Empty,
                    Processor = reader["Processor"] as string,
                    Ram = reader["Ram"] as string,
                    Storage = reader["Storage"] as string,
                    Price = reader["Price"] == DBNull.Value ? null : Convert.ToDecimal(reader["Price"]),
                    Status = reader["Status"] as string
                });
            }
        }
    }
}
