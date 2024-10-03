using Domain.Courses;
using Domain.UserCourse;
using Domain.Users;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
{
    public void Configure(EntityTypeBuilder<UserCourse> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new UserCourseId(x))
            .IsRequired();

        builder.Property(x => x.CourseId)
            .HasConversion(x => x.Value, x => new CourseId(x))
            .IsRequired();
        builder.HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Rating)
            .HasColumnType("varchar(255)");

        builder.Property(x => x.JoinAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired();

        builder.Property(x => x.EndAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired();
        
    }
}