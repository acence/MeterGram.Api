using MeterGram.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeterGram.Infrastructure.Database.Configuration.Models
{
    public class ParticipantEntityConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("Participants");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.Phone)
                .HasMaxLength(50);

            builder
                .Property(x => x.Email)
                .HasMaxLength(100);
        }
    }
}