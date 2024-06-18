using WebApplication1.CosmosDB;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using WebApplication1.Common;
using WebApplication1.Interface;
// Make sure to include this

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IManagerService , ManagerService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<ISecurityService , SecurityService>();   
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>(); // Assuming CosmosDbService has a parameterless constructor
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
