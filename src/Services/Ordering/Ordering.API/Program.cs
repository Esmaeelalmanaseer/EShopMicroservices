using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApiService(builder.Configuration);
var app = builder.Build();


app.AddApi();
if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.Run();
