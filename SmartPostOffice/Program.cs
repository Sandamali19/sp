using Microsoft.EntityFrameworkCore;
using SmartPostOffice.Models; // Mathaka athuwa ube project name eka hariyata dapan

var builder = WebApplication.CreateBuilder(args);

// 1. Connection String eka kiyawanna
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Database Context eka register karamu (post_db nisa context eka post_dbContext kiyala thiyenne)
builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
// Static files (CSS, JS) use karanna meka ona
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();

// Default route eka - Home Controller eke Index action ekata yanawa
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();