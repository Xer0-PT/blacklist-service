namespace BlackList.Persistence.Data.Mappings;

using BlackList.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.BlackListedPlayers)
            .WithOne(y => y.User)
            .HasForeignKey(y => y.UserId);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");
        
        builder
            .Property(x => x.Token)
            .HasColumnName("token");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("createdAt");

    }
}
