using lab2;

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

builder.Configuration.AddJsonFile("configs/myconfig.json");

if (microsoft.nperson > apple.nperson && microsoft.nperson > google.nperson) { app.MapGet("1/", (IConfiguration appConfig) => $"{microsoft.name} - {microsoft.nperson}"); }
else if (microsoft.nperson < apple.nperson && apple.nperson > google.nperson) { app.MapGet("1/", (IConfiguration appConfig) => $"{apple.name} - {apple.nperson}"); }
else if (google.nperson > apple.nperson && google.nperson > microsoft.nperson) { app.MapGet("1/", (IConfiguration appConfig) => $"{google.name} - {google.nperson}"); }

app.MapGet("2/", (IConfiguration appConfig) => $"Name -{appConfig["person"]} \n" +
                                            $"Sex -{appConfig["sex"]} \n" +
                                            $"Age -{appConfig["age"]}");
app.Run();
