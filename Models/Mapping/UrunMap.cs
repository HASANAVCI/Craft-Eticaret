using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CraftTicaret.WebUI.Models.Mapping
{
    public class UrunMap : EntityTypeConfiguration<Urun>
    {
        public UrunMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Aciklama)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Urun");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.Fiyat).HasColumnName("Fiyat");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.KategoriId).HasColumnName("KategoriId");
            this.Property(t => t.MarkaId).HasColumnName("MarkaId");

            // Relationships
            this.HasOptional(t => t.Kategori)
                .WithMany(t => t.Uruns)
                .HasForeignKey(d => d.KategoriId);
            this.HasOptional(t => t.Marka)
                .WithMany(t => t.Uruns)
                .HasForeignKey(d => d.MarkaId);

        }
    }
}
