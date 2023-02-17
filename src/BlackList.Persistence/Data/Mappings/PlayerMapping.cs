using BlackList.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackList.Persistence.Data.Mappings;

public class PlayerMapping : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("player");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.UserId)
            .HasColumnName("userId");

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.FaceitId)
            .HasColumnName("faceitId");

        builder
            .Property(x => x.Nickname)
            .HasColumnName("nickName");

        builder
            .Property(x => x.Banned)
            .HasColumnName("banned");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("createdAt");
    }
}
