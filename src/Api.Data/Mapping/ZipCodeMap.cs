using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ZipCodeMap : IEntityTypeConfiguration<ZipCodeEntity>
    {
        public void Configure(EntityTypeBuilder<ZipCodeEntity> builder)
        {
            builder.ToTable("ZipCode");

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.ZipCode);

            builder.HasOne(z => z.City).WithMany(c => c.ZipCodes);
        }
    }
}
