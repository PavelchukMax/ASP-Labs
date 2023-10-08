using lab5;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));
var app = builder.Build();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync($"<div>");
    await context.Response.WriteAsync($"<p><a href='/add'>Додання даних</a></p>");
    await context.Response.WriteAsync($"<p><a href='/check'>Перевірка даних</a></p>");
    await context.Response.WriteAsync($"</div>");

});

app.MapGet("/add", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync($"<p>Додайте дані</p>");
    await context.Response.WriteAsync($"<form id=\"dataform\" action=\"/set-data\" method=\"post\">");
    await context.Response.WriteAsync($"<div>");
    await context.Response.WriteAsync($"<label>Значення:</label>");
    await context.Response.WriteAsync($"<input type=\"text\" id=\"valueIn\" name=\"valueIn\" required>");
    await context.Response.WriteAsync($"<label>Дата видалення даних:</label>");
    await context.Response.WriteAsync($"<input type=\"localDatetime\" id=\"destroyDate\" name=\"destroyDate\" required>");
    await context.Response.WriteAsync($"<button type=\"submit\">Äîäàòè</button>");
    await context.Response.WriteAsync($"</div>");
    await context.Response.WriteAsync($"</form>");

});
app.MapPost("/set-data", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    var value = context.Request.Form["valueIn"];
    var destroyDate = context.Request.Form["destroyDate"];
    if (DateTime.Parse(destroyDate) < DateTime.Now)
    {
        await context.Response.WriteAsync($"<p>Значення \"{value}\"не було збережено, бо дата життя даних була вичерпана.</p>");
        await context.Response.WriteAsync($"<a href='/'>Додому</a> <br/>");
        await context.Response.WriteAsync("<a href='/add'>Додати нові дані</a>");
        throw new ApplicationException("Wrong destroy date for data");
    }
    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(destroyDate) && DateTime.TryParse(destroyDate, out var destroy))
    {
        var options = new CookieOptions
        {
            Expires = destroy,
            IsEssential = true,
        };

        context.Response.Cookies.Append("datas", value, options);

        await context.Response.WriteAsync($"<p>Значення \"{value}\" було збережено.</p>");
        await context.Response.WriteAsync($"<a href='/'>Додому</a> <br/>");
        await context.Response.WriteAsync("<a href='/add'>Додати нові дані</a>");
    }
    else
    {
        await context.Response.WriteAsync("Помилка: Не вдалося зберегти дані");
        await context.Response.WriteAsync("<a href='/'>Додому</a>"); 
        await context.Response.WriteAsync("<a href='/add'>Додати нові дані</a>");
    }
});

app.MapGet("/check", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    if (context.Request.Cookies.TryGetValue("datas", out var value))
    {
        await context.Response.WriteAsync($"Данні: {value}.");
    }
    else
    {
        await context.Response.WriteAsync($"Немає збережених данних.");
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
