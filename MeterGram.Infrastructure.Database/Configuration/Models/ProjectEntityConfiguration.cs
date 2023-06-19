using MeterGram.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeterGram.Infrastructure.Database.Configuration.Models
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.CourseName)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(false);

            builder
                .HasMany(x => x.CompanyApplications)
                .WithOne(x => x.Project);
        }
    }
}