using BlackList.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackList.Persistence.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Players)
            .WithOne(y => y.User);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.FaceitId)
            .HasColumnName("faceitId");

        builder
            .Property(x => x.Nickname)
            .HasColumnName("nickname");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("createdAt");

    }
}
