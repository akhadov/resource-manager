using Microsoft.EntityFrameworkCore;
using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;

namespace ResourceManager.Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentHistory> Histories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}

