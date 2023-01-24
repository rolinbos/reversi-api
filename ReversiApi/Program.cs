using Microsoft.EntityFrameworkCore;
using ReversiApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
var reversiConnectionString = builder.Configuration.GetConnectionString("apiConnection");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite(reversiConnectionString));

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    // Here you can change the cors to only allow a certain site.
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();