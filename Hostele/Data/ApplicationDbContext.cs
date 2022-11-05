using Hostele.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Data;

public class ApplicationDbContext:IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Hostel> Hostele { get; set; }
    public DbSet<Pokoj> Pokoje { get; set; }
    public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
    public DbSet<Rodzaj> Rodzaje { get; set; }
    public List<Aktywnosc> Aktywnosci { get; set; }
    
}