using System;
using System.Collections.Generic;

namespace CraftTicaret.WebUI.Models
{
    public partial class Sati
    {
        public Sati()
        {
            this.SatisDetays = new List<SatisDetay>();
        }

        public int Id { get; set; }
        public Nullable<System.Guid> MusteriId { get; set; }
        public System.DateTime SatisTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public bool SepetteMi { get; set; }
        public Nullable<int> KargoId { get; set; }
        public Nullable<int> SiparisDurumId { get; set; }
        public string KargoTakipNo { get; set; }
        public virtual Kargo Kargo { get; set; }
        public virtual Musteri Musteri { get; set; }
        public virtual SiparisDurum SiparisDurum { get; set; }
        public virtual ICollection<SatisDetay> SatisDetays { get; set; }
    }
}
