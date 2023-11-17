using lab_11.Filters;
using lab_11.Services.WeatherService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTransient<IWeatherService, WeatherService>()
    .AddHttpClient()
    .AddControllersWithViews(options =>
    {
        options.Filters.Add(typeof(LogActionAttribute)); // ������� ������ ��� ��������� ��
        options.Filters.Add(typeof(UserCountAttribute)); // ������� ������ ��� ��������� ������� ��������� ������������
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
