using System.Net;

namespace Orcafin._UnitTests
{
    public class Class1
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(nameof(Address));
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.CEP).IsRequired();
            builder.Property(a => a.CEP).HasMaxLength(16);
            builder.Property(a => a.StreetName).HasMaxLength(128);
            builder.Property(a => a.Number).HasMaxLength(32);
            builder.Property(a => a.Neighborhood).HasMaxLength(64);
            builder.Property(a => a.City).HasMaxLength(64);
            builder.Property(a => a.State).HasMaxLength(32);
            builder.Property(a => a.Complement).HasMaxLength(128);
            builder.HasOne(a => a.Person).WithMany(u => u.Addresses).HasForeignKey(a => a.PersonId).IsRequired();
        }
    }
}
}
