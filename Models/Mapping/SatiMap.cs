using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CraftTicaret.WebUI.Models.Mapping
{
    public class SatiMap : EntityTypeConfiguration<Sati>
    {
        public SatiMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.KargoTakipNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Satis");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MusteriId).HasColumnName("MusteriId");
            this.Property(t => t.SatisTarihi).HasColumnName("SatisTarihi");
            this.Property(t => t.ToplamTutar).HasColumnName("ToplamTutar");
            this.Property(t => t.SepetteMi).HasColumnName("SepetteMi");
            this.Property(t => t.KargoId).HasColumnName("KargoId");
            this.Property(t => t.SiparisDurumId).HasColumnName("SiparisDurumId");
            this.Property(t => t.KargoTakipNo).HasColumnName("KargoTakipNo");

            // Relationships
            this.HasOptional(t => t.Kargo)
                .WithMany(t => t.Satis)
                .HasForeignKey(d => d.KargoId);
            this.HasOptional(t => t.Musteri)
                .WithMany(t => t.Satis)
                .HasForeignKey(d => d.MusteriId);
            this.HasOptional(t => t.SiparisDurum)
                .WithMany(t => t.Satis)
                .HasForeignKey(d => d.SiparisDurumId);

        }
    }
}
