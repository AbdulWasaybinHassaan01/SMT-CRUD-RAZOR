using CRUD_Razor.Data;

var builder = WebApplication.CreateBuilder(args);

// Ensure default culture uses US dollar ($) format
var cultureInfo = new System.Globalization.CultureInfo("en-US");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Initialize SQLite database and tables automatically
var connectionString = app.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=computers.db";
DatabaseInitializer.Initialize(connectionString);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
