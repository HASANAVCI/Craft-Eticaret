using System;
using System.Collections.Generic;

namespace CraftTicaret.WebUI.Models
{
    public partial class Urun
    {
        public Urun()
        {
            this.Resims = new List<Resim>();
            this.SatisDetays = new List<SatisDetay>();
            this.UrunOzelliks = new List<UrunOzellik>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public decimal Fiyat { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public Nullable<int> MarkaId { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual Marka Marka { get; set; }
        public virtual ICollection<Resim> Resims { get; set; }
        public virtual ICollection<SatisDetay> SatisDetays { get; set; }
        public virtual ICollection<UrunOzellik> UrunOzelliks { get; set; }
    }
}
