using WebApplication1.Entities;
using WebApplication1.Dto;
using WebApplication1.Endpoints;




var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGamesEndpoints();



app.Run();
