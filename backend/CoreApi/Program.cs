using CoreApi.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// Adiciona política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
// Registrando o DbContext com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("PermitirTudo"); // Aqui ativa o CORS antes da autorização
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();