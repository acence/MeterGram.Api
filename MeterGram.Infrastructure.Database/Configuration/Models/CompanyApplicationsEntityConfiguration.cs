using MeterGram.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeterGram.Infrastructure.Database.Configuration.Models
{
    public class CompanyApplicationsEntityConfiguration : IEntityTypeConfiguration<CompanyApplication>
    {
        public void Configure(EntityTypeBuilder<CompanyApplication> builder)
        {
            builder.ToTable("CompanyApplications");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.Phone)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasMany(x => x.Participants)
                .WithOne(x => x.CompanyApplication);
        }
    }
}