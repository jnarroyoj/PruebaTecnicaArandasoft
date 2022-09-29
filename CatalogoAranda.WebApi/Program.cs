using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using CatalogoAranda.ApplicationCore.UtilityServices;
using CatalogoAranda.ApplicationCore.UtilityServices.Interfaces;
using CatalogoAranda.Infrastructure.Data;
using CatalogoAranda.Infrastructure.UnitOfWork.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
options.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region Configuración de documentación de swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Prueba técnica Arandasoft",
        Version = "1.0",
        Description = "Documentación de la prueba técnica de catálogo.",
        Contact = new OpenApiContact
        {
            Name = "José Nicolás Arroyo Jiménez",
            Email = "jnarroyoj@gmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "Repositorio",
            Url = new Uri("https://github.com/jnarroyoj/PruebaTecnicaArandasoft")
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] { }
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
});

# endregion

# region Inyección de dependencias de capa de aplicación

builder.Services.AddScoped<ICategoriasService, CategoriasService>()
    .AddScoped<IProductosService, ProductosService>()
    .AddScoped<IImagenesService, ImagenesService>()
    .AddScoped<IAuthenticationService, AuthenticationService>();

# endregion

# region Inyección de dependencias de capa de infraestructura

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<CatalogoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"),
        b => b.MigrationsAssembly("CatalogoAranda.Infrastructure")));

# endregion

#region Configuración de autenticación

builder.Services.AddIdentity<CatalogoUser, IdentityRole>()
    .AddEntityFrameworkStores<CatalogoDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["LlaveSimetrica"])),
    });

#endregion 

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
