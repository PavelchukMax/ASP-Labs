var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Random x = new Random();
lab1.Company company = new lab1.Company("Microsoft",5000, "This is the company that created Windows");

app.MapGet("1/", () => "Hello World!");
app.MapGet("2/", () => $"Company name {company.name} Company nworkers {company.nworkers} Company description {company.description}");
int n = x.Next(101);
app.MapGet("3/", () => $"random number 0-100: {n}");
app.MapGet("3/", () => $"random number 0-100: {n}");


app.Run();
