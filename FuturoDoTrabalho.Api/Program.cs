using FuturoDoTrabalho.Api.Data;
using FuturoDoTrabalho.Api.Repositories;
using FuturoDoTrabalho.Api.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 1. CONFIGURAÇÃO DO EF CORE
// =======================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =======================================
// 2. REPOSITORIES & SERVICES (DI)
// =======================================

// Trilhas
builder.Services.AddScoped<ITrilhaRepository, TrilhaRepository>();
builder.Services.AddScoped<ITrilhaService, TrilhaService>();

// Usuários
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// =======================================
// 3. API CONTROLLERS
// =======================================
builder.Services.AddControllers();

// =======================================
// 4. VERSIONAMENTO DE API
// =======================================
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

    // versão na URL: /api/v1/trilhas
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Necessário para o Swagger entender as versões
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";         // v1, v2...
    options.SubstituteApiVersionInUrl = true;   // substitui {version:apiVersion}
});

// =======================================
// 5. SWAGGER
// =======================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Cria o banco e aplica todas as migrations pendentes
    db.Database.Migrate();
}

// =======================================
// 6. MIDDLEWARES
// =======================================

// Swagger SEM depender do ambiente
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Futuro do Trabalho API v1");

    // se depois fizermos uma v2, adiciona aqui:
    // options.SwaggerEndpoint("/swagger/v2/swagger.json",
    //     "Futuro do Trabalho API v2");

    options.RoutePrefix = string.Empty; 
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
