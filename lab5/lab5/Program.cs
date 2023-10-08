using lab5;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));
var app = builder.Build();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync($"<div>");
    await context.Response.WriteAsync($"<p><a href='/add'>������� �����</a></p>");
    await context.Response.WriteAsync($"<p><a href='/check'>�������� �����</a></p>");
    await context.Response.WriteAsync($"</div>");

});

app.MapGet("/add", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync($"<h1>������� ���</h1>");
    await context.Response.WriteAsync($"<form id=\"dataform\" action=\"/set-data\" method=\"post\">");
    await context.Response.WriteAsync($"<div>");
    await context.Response.WriteAsync($"<label for=\"valueIn\">��������:</label>");
    await context.Response.WriteAsync($"<input type=\"text\" id=\"valueIn\" name=\"valueIn\" required>");
    await context.Response.WriteAsync($"</div>");
    await context.Response.WriteAsync($"<div>");
    await context.Response.WriteAsync($"<label for=\"destroyDate\">���� ��������� �����:</label>");
    await context.Response.WriteAsync($"<input type=\"localDatetime\" id=\"destroyDate\" name=\"destroyDate\" required>");
    await context.Response.WriteAsync($"</div>");
    await context.Response.WriteAsync($"<div>");
    await context.Response.WriteAsync($"<button type=\"submit\">������</button>");
    await context.Response.WriteAsync($"</div>");
    
});
app.MapPost("/set-data", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    var value = context.Request.Form["valueIn"];
    var destroyDate = context.Request.Form["destroyDate"];
    if (DateTime.Parse(destroyDate) < DateTime.Now)
    {
        await context.Response.WriteAsync($"<p>�������� \"{value}\" �� ���� ���������, �� ���� ����� ����� ���� ���������.</p>");
        throw new ApplicationException("Wrong expiration date for data");
    }
    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(destroyDate) && DateTime.TryParse(destroyDate, out var destroy))
    {
        var options = new CookieOptions
        {
            Expires = destroy,
            IsEssential = true,
        };

        context.Response.Cookies.Append("datas", value, options);

        await context.Response.WriteAsync($"<p>�������� \"{value}\" ���� ���������.</p>");
        await context.Response.WriteAsync($"<a href='/'>Home</a> <br/>");
        await context.Response.WriteAsync("<a href='/add'>add new data</a>");
    }
    else
    {
    await context.Response.WriteAsync("�������: �� ������� �������� ���");
        await context.Response.WriteAsync("<a href='/'>Home</a>"); 
        await context.Response.WriteAsync("<a href='/add'>add new data</a>");
    }
});

app.MapGet("/check", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    if (context.Request.Cookies.TryGetValue("datas", out var value))
    {
        await context.Response.WriteAsync($"����: {value}.");
    }
    else
    {
        await context.Response.WriteAsync($"���� ���������� ������.");
        throw new ApplicationException("No data");
    }
});
app.Use(async (HttpContext context, RequestDelegate result) =>
{
    try
    {
        await result.Invoke(context);
    }
    catch(Exception exception)
    {
        var logger = app.Logger;
        var time = DateTime.Now.ToString();
        logger.LogError(time+" : "+ exception.Message);
        throw;
    }
});
app.Run();
