using System;
using System.Collections.Generic;

namespace CraftTicaret.WebUI.Models
{
    public partial class OzellikDeger
    {
        public OzellikDeger()
        {
            this.UrunOzelliks = new List<UrunOzellik>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int OzellikTipId { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }
        public virtual ICollection<UrunOzellik> UrunOzelliks { get; set; }
    }
}
