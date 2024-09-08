using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;

namespace ResourceManager.Infrastructure.Database.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasMany(h => h.Histories)
            .WithOne()
            .HasForeignKey(d => d.Id);

        builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
