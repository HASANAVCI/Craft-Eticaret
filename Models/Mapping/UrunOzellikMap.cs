using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CraftTicaret.WebUI.Models.Mapping
{
    public class UrunOzellikMap : EntityTypeConfiguration<UrunOzellik>
    {
        public UrunOzellikMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UrunId, t.OzellikTipId, t.OzellikDegerId });

            // Properties
            this.Property(t => t.UrunId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OzellikTipId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OzellikDegerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UrunOzellik");
            this.Property(t => t.UrunId).HasColumnName("UrunId");
            this.Property(t => t.OzellikTipId).HasColumnName("OzellikTipId");
            this.Property(t => t.OzellikDegerId).HasColumnName("OzellikDegerId");

            // Relationships
            this.HasRequired(t => t.OzellikDeger)
                .WithMany(t => t.UrunOzelliks)
                .HasForeignKey(d => d.OzellikDegerId);
            this.HasRequired(t => t.OzellikTip)
                .WithMany(t => t.UrunOzelliks)
                .HasForeignKey(d => d.OzellikTipId);
            this.HasRequired(t => t.Urun)
                .WithMany(t => t.UrunOzelliks)
                .HasForeignKey(d => d.UrunId);

        }
    }
}
