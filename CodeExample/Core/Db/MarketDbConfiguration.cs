using CodeExample.Models;

namespace CodeExample.Core.Db
{
    public class MarketDbConfiguration : IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.ToTable("Market");

            builder.Property(m => m.Id)
                .HasColumnName("Market_id");

            builder.Property(m => m.Name)
                .HasColumnName("Market_Name")
                .HasMaxLength(Market.NameMaxLength)
                .IsRequired();

            builder.Property(m => m.IsReserveMarket)
                .HasColumnName("Is_Reserve_Market");

            builder.Property(m => m.Description)
                .HasColumnName("Market_Description")
                .HasMaxLength(Market.DescriptionMaxLength);

            builder.Property(m => m.Classification)
                .HasColumnName("Market_Classification")
                .HasConversion(c => c != null ? c.Value.ToString().ToLower() : null, s => s != null ? Enum.Parse<MarketClassification>(s, true) : null);
        }
    }
}
