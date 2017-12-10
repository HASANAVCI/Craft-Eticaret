using System;
using System.Collections.Generic;

namespace CraftTicaret.WebUI.Models
{
    public partial class Marka
    {
        public Marka()
        {
            this.Uruns = new List<Urun>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> ResimId { get; set; }
        public virtual Resim Resim { get; set; }
        public virtual ICollection<Urun> Uruns { get; set; }
    }
}
