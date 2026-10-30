using Microsoft.Data.Sqlite;

namespace CRUD_Razor.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS Computers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Brand TEXT NOT NULL,
                    Model TEXT NOT NULL,
                    Processor TEXT NULL,
                    Ram TEXT NULL,
                    Storage TEXT NULL,
                    Price REAL NULL,
                    Status TEXT NULL
                );";

            using (var command = new SqliteCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }

            // Check if table is empty, and seed sample data if needed
            string countSql = "SELECT COUNT(*) FROM Computers";
            using (var countCmd = new SqliteCommand(countSql, connection))
            {
                long count = (long)(countCmd.ExecuteScalar() ?? 0);
                if (count == 0)
                {
                    string seedSql = @"
                        INSERT INTO Computers (Brand, Model, Processor, Ram, Storage, Price, Status) VALUES
                        ('Dell', 'XPS 15 (9530)', 'Intel Core i7-13700H', '32 GB DDR5', '1 TB NVMe SSD', 1899.99, 'In Stock'),
                        ('Apple', 'MacBook Pro 16""', 'Apple M3 Pro (12-core)', '36 GB Unified', '512 GB SSD', 2499.00, 'In Use'),
                        ('Lenovo', 'ThinkPad X1 Carbon Gen 11', 'Intel Core i7-1365U', '16 GB LPDDR5', '512 GB NVMe SSD', 1429.50, 'In Stock'),
                        ('ASUS', 'ROG Zephyrus G16', 'Intel Core Ultra 9 185H', '32 GB LPDDR5X', '1 TB NVMe SSD', 1999.99, 'Maintenance');";

                    using var seedCmd = new SqliteCommand(seedSql, connection);
                    seedCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
