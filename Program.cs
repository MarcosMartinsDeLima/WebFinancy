using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebFinancy.Config;
using WebFinancy.Controllers;
using WebFinancy.Model.Context;
using WebFinancy.Repository;
using WebFinancy.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Connection to mysql
var Connection = builder.Configuration["MysqlConnection:MysqlConnectionString"];
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(Connection, new MySqlServerVersion(new Version(8,0,5))) );

//mapper
builder.Services.AddAutoMapper(typeof(Program));
IMapper mapper = MapConfig.RegisterMapping().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//injetando repositorios
builder.Services.AddScoped<IFinancyRepository,FinancyRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddTransient<JwtService>();
builder.Services.AddHttpClient();

//jwt support
var key = Encoding.ASCII.GetBytes("secretosecretosecretoseraqueagoravaiesperoquesim@34");
builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => 
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{ 
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }}
    );
    });


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
