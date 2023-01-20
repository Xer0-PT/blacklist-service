namespace BlackList.Persistence.Data.Mappings;

using BlackList.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BlackListedPlayerMapping : IEntityTypeConfiguration<BlackListedPlayer>
{
    public void Configure(EntityTypeBuilder<BlackListedPlayer> builder)
    {
        builder.ToTable("blackListedPlayer");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Users)
            .WithMany(y => y.BlackListedPlayers)
            .UsingEntity(x => x.ToTable("userBlackListedPlayer"));

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
            .Property(x => x.CreatedAt)
            .HasColumnName("createdAt");
    }
}
