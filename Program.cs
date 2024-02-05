using Microsoft.EntityFrameworkCore;
using WebFinancy.Model.Context;
using WebFinancy.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Connection to mysql
var Connection = builder.Configuration["MysqlConnection:MysqlConnectionString"];
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(Connection, new MySqlServerVersion(new Version(8,0,5))) );

//injetando repositorios
builder.Services.AddScoped<IFinancyRepository,FinancyRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
