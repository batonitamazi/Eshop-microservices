using Ordering.API;
using Ordering.Application;
using Ordering.Instrastructure;
var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();


//Configure the HTTP Request Pipeline

app.Run();
