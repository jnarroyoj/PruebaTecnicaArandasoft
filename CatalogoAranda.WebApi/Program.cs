using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using CatalogoAranda.Infrastructure.Data;
using CatalogoAranda.Infrastructure.UnitOfWork.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
options.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

# region Inyecci�n de dependencias de capa de aplicaci�n

builder.Services.AddScoped<ICategoriasService, CategoriasService>()
    .AddScoped<IProductosService, ProductosService>()
    .AddScoped<IImagenesService, ImagenesService>();

# endregion

# region Inyecci�n de dependencias de capa de infraestructura

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<CatalogoDbContext>();

# endregion

#region Configuraci�n de autenticaci�n

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "PruebaT�cnicaArandasoft",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["key"])),
        ClockSkew = TimeSpan.Zero
    });

builder.Services.AddIdentity<CatalogoUser, IdentityRole>()
    .AddEntityFrameworkStores<CatalogoDbContext>()
    .AddDefaultTokenProviders();

#endregion 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
