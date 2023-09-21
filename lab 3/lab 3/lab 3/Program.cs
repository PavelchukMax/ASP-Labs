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
    await context.Response.WriteAsync($" Сума {calc?.Sum(a,b)} ");
    await context.Response.WriteAsync($"\nРізниця {calc?.Difference(a,b)} ");
    await context.Response.WriteAsync($"\nМноження {calc?.Product(a,b)} ");
    await context.Response.WriteAsync($"\nДілення {calc?.Quotient(a,b)} ");
    await context.Response.WriteAsync($"\nСтепінь {calc?.Pow(a,b)} ");
    await context.Response.WriteAsync($"\n {timeService?.Daytime()} ");
});

app.Run();
