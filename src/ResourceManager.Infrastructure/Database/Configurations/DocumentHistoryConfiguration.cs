using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;

namespace ResourceManager.Infrastructure.Database.Configurations;

internal sealed class DocumentHistoryConfiguration : IEntityTypeConfiguration<DocumentHistory>
{
    public void Configure(EntityTypeBuilder<DocumentHistory> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
