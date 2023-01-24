using Microsoft.EntityFrameworkCore;
using ReversiApi.Models;

namespace ReversiApi.Data;

public class ApiDbContext: DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
    {}
    
    public DbSet<Spel> Spels { get; set; }
}