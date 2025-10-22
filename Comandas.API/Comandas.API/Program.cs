using Comandas.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Configurar o contexto do banco de cados para usar InMemory Database
builder.Services.AddDbContext<ComandasDbContext>(options =>
    options.UseInMemoryDatabase("ComandasDb")
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o servi�o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPolitica", policy =>
    {
        policy.WithOrigins("http://localhost", "http://127.0.0.1:5500", "http://127.0.0.1") // Origens permitidas
        .AllowAnyHeader() // Permite qualquer cabe�alho
        .AllowAnyMethod(); // Permite qualquer m�todo HTTP
    });
});

var app = builder.Build();

// Configura o middleware CORS


app.UseCors("MinhaPolitica");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
