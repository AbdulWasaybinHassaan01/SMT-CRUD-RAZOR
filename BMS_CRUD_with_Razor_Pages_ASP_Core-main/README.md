# Computers Management System (ASP.NET Core Razor Pages + SQLite)

A clean, modern, and lightweight Computer Inventory & Workstation Management System built with **ASP.NET Core Razor Pages** and **SQLite**.

> [!NOTE]
> **Zero Database Configuration Required**: No Microsoft SQL Server or SQL Server Management Studio (SSMS) is needed. SQLite automatically creates and maintains the embedded `computers.db` database file on application startup.

---

## 🚀 Features

- **Create**: Add new computers with specifications (Brand, Model, Processor, RAM, Storage, Price, Inventory Status).
- **Read & Search**: Instant table list with keyword search across Brand, Model, Processor, or Status.
- **View Details**: Dedicated specifications view page for each workstation.
- **Edit**: Update hardware specifications, pricing, and inventory statuses.
- **Delete**: Safe deletion with full hardware confirmation details.
- **Auto-Initialization**: Auto-creates the SQLite table and seeds starter records on the first run.
- **Modern Responsive UI**: Styled with Bootstrap 5.3, Bootstrap Icons, and clean cards.

---

## 🗄️ Database Schema (`computers.db`)

```sql
CREATE TABLE IF NOT EXISTS Computers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Brand TEXT NOT NULL,
    Model TEXT NOT NULL,
    Processor TEXT NULL,
    Ram TEXT NULL,
    Storage TEXT NULL,
    Price REAL NULL,
    Status TEXT NULL
);
```

---

## 💻 How to Run

1. Open PowerShell or Terminal in this folder.
2. Run the application:
   ```bash
   dotnet run --project CRUD_Razor/CRUD_Razor.csproj
   ```
   *Alternatively, if running from inside the `CRUD_Razor` folder:*
   ```bash
   cd CRUD_Razor
   dotnet run
   ```
3. Open your browser and navigate to the displayed URL (e.g. `http://localhost:5000` or `https://localhost:5001`).
