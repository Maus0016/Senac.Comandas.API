using Comandas.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ComandasDbContext>(options =>
    options.UseSqlite("DataSource=Comandas.db")
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - ALLOW EVERYTHING FOR DEVELOPMENT
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Criar o banco de dados 
// Criar um escopo usando dadods para obter instancias de variaveis

using (var scope = app.Services.CreateScope())
{
    //Obtem um objeteto de bamco de dados
    var db = scope.ServiceProvider.GetRequiredService<ComandasDbContext>();
    // Executa as migraçoes no banco de dados
    await db.Database.MigrateAsync();
}

// Use CORS
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();