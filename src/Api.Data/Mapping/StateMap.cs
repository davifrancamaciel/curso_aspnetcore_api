using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class StateMap : IEntityTypeConfiguration<StateEntity>
    {
        public void Configure(EntityTypeBuilder<StateEntity> builder)
        {
            builder.ToTable("State");

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Sigla).IsUnique();

            // builder.Property(u => u.Name).IsRequired().HasMaxLength(45);
            // builder.Property(u => u.Sigla).IsRequired().HasMaxLength(2);
        }
    }
}
