using Domain.Users;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(x => x.FirstName).IsRequired().HasColumnType("varchar(255)");
        builder.Property(x => x.LastName).IsRequired().HasColumnType("varchar(255)");
        builder.Ignore(x => x.FullName);

        builder.Property(x => x.UpdatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");

        builder.HasOne(x => x.Faculty)
            .WithMany()
            .HasForeignKey(x => x.FacultyId)
            .HasConstraintName("fk_users_faculties_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}