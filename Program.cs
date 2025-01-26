using System.Text;
using CadastroServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsuarioApi.Authorization;
using UsuarioApi.Data;
using UsuarioApi.Models;
using UsuarioApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão
var conneString = builder.Configuration["ConnectionStrings:UsuarioConnection"];
builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    opts.UseMySql(conneString, ServerVersion.AutoDetect(conneString));
});

// Configuração do Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

// Configuração de autorização
builder.Services.AddAuthorization();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
    policy.AddRequirements(new IdadeMinima(18))
    );
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();


var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de autenticação e autorização
app.UseAuthentication();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
