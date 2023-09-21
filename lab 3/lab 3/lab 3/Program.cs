using lab_3.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITimeService ,TimeService>();
builder.Services.AddTransient<ICalcService, CalcService>();
int a = 8;
int b = 4;
var app = builder.Build();
app.Run(async context =>
{
    context.Response.Headers["Content-Type"] = "text/plain; charset=utf-8";
    var timeService = app.Services.GetService<ITimeService>();
    var calc= app.Services.GetService<ICalcService>();
    await context.Response.WriteAsync($" ���� {calc?.Sum(a,b)} ");
    await context.Response.WriteAsync($"\nг����� {calc?.Difference(a,b)} ");
    await context.Response.WriteAsync($"\n�������� {calc?.Product(a,b)} ");
    await context.Response.WriteAsync($"\nĳ����� {calc?.Quotient(a,b)} ");
    await context.Response.WriteAsync($"\n������ {calc?.Pow(a,b)} ");
    await context.Response.WriteAsync($"\n {timeService?.Daytime()} ");
});

app.Run();
