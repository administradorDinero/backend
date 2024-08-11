using apiCuentas.helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositorio;
using Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ServicePersona>();
builder.Services.AddScoped<AuthHelpers>();
builder.Services.AddScoped<ServicioCuenta>();
builder.Services.AddScoped<ServicioTransacciones>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    //Configuracion de la open api para la autenticación de token en swagger.
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:5173") // Cambia esto a la URL de tu frontend
            .AllowAnyMethod()
            .AllowAnyHeader());
});



builder.Services.AddAuthentication("Bearer").AddJwtBearer(x=>
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            //Configuración de la llave para la validación del token.
            .GetBytes("this is my custom Secret key for authentication")
        ),
         ValidateIssuer = false,
         ValidateAudience = false,
         ClockSkew = TimeSpan.Zero
     });
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=192.168.1.58:5432;Database=AdministradorDinero;Username=yourusername;Password=yourpassword")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
