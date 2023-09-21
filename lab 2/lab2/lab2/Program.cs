using CompanyClass;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration.AddXmlFile("configs/config.xml");

var apple = new Company();
app.Configuration.Bind(apple);

builder.Configuration.AddJsonFile("configs/config.json");
var microsoft =new Company();
app.Configuration.Bind(microsoft);

builder.Configuration.AddIniFile("configs/config.ini");
var google = new Company();
app.Configuration.Bind(google);
Company[] companies = new Company[3];
companies[0] = microsoft; 
companies[1] = apple;
companies[2] = google;
int Id_max=0;
for (int i = 0; i < companies.Length-1; i++)
{
if (companies[i].Nperson < companies[i+1].Nperson) { Id_max = i + 1; } else { Id_max = i; } 
} 
builder.Configuration.AddJsonFile("configs/myconfig.json");
app.MapGet("1/", (IConfiguration appConfig) => $"{companies[Id_max].Name} - {companies[Id_max].Nperson}");
app.MapGet("2/", (IConfiguration appConfig) => $"Name -{appConfig["person"]} \n" +
                                            $"Sex -{appConfig["sex"]} \n" +
                                            $"Age -{appConfig["age"]}");
app.Run();
