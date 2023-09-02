using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DependancyResolver;
using businessLayer.DependancyResolver;
using DataAccessLayer.Models;
using StorePro.Utilities.CustomMiddleWares;
using System.Net;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("WebApplication1ContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication1ContextConnection' not found.");


builder.Services.ConfigureDependanciesDataAccessLayer(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.ConfigureDependancieseBusinessLayer(builder.Configuration);



builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors",
        builder =>
        {
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
          
            .AllowAnyOrigin();
        

        });
});
// Add services to the container.
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//app.UseStatusCodePagesWithRedirects("");



app.UseStatusCodePages(context => {
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.Redirect("/account/login");
    }
    if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        response.Redirect("/Default/NotFound");
    }
    if (response.StatusCode == (int)HttpStatusCode.BadRequest)
    {
        response.Redirect("/Default/BadRequest");
    }

    return Task.CompletedTask;
});


app.UseHttpsRedirection();


//app.UseMiddleware<ExceptionMiddleware>();
app.UseExceptionHandler("/Error");


app.UseStaticFiles();
app.UseCors("cors");

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
