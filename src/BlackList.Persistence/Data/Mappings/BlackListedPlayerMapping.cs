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
            .HasOne(x => x.User)
            .WithMany(y => y.BlackListedPlayers)
            .HasForeignKey(x => x.UserId);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.UserId)
            .HasColumnName("userId");

        builder
            .Property(x => x.Nickname)
            .HasColumnName("nickName");
        
        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("createdAt");
    }
}
