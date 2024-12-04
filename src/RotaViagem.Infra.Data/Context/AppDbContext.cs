using Microsoft.EntityFrameworkCore;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Rota> Rotas { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../rota.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
