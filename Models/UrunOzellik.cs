using System;
using System.Collections.Generic;

namespace CraftTicaret.WebUI.Models
{
    public partial class UrunOzellik
    {
        public int UrunId { get; set; }
        public int OzellikTipId { get; set; }
        public int OzellikDegerId { get; set; }
        public virtual OzellikDeger OzellikDeger { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }
        public virtual Urun Urun { get; set; }
    }
}
