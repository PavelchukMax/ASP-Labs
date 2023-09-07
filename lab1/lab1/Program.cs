var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Random x = new Random();
Company company= new Company("Microsoft",5000, "This is the company that created Windows");

app.MapGet("1/", () => "Hello World!");
app.MapGet("2/", () => $"Company name {company.name} Company nworkers {company.nworkers} Company description {company.description}");
int n = x.Next(101);
app.MapGet("3/", () => $"random number 0-100: {n}");
app.MapGet("3/", () => $"random number 0-100: {n}");


app.Run();
class Company
{
    public string name;
    public int nworkers;
    public string description;
    public Company(string name, int nworkers, string description)
    {
        this.name = name;
        this.nworkers = nworkers;
        this.description = description;
    }
}