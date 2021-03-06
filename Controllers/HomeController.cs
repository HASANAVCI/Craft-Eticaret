﻿using CraftTicaret.WebUI.App_Classes;
using CraftTicaret.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CraftTicaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Sepet()
        {
            return PartialView();
        }
        public PartialViewResult Slider()
        {
            var data = Context.Baglanti.Resims.Where(x => x.BuyukYol.Contains("Slider")).ToList();
            return PartialView(data);
        }
        public PartialViewResult YeniUrunler()
        {
            var data = Context.Baglanti.Uruns.ToList();
            return PartialView(data);
        }
        public PartialViewResult Servisler()
        {
            return PartialView();
        }
        public PartialViewResult Markalar()
        {
            var data = Context.Baglanti.Markas.ToList();
            return PartialView(data);
        }
        public void SepeteEkle(int id)
        {
            SepetItem si = new SepetItem();
            Urun u = Context.Baglanti.Uruns.FirstOrDefault(x => x.Id == id);
            si.Urun = u;
            si.Adet = 1;
            si.Indirim = 0;
            Sepet s = new Sepet();
            s.SepeteEkle(si);
        }
        public void SepetUrunAdetDusur(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];
                if (s.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet > 1)
                {
                    s.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet--;
                }
                else
                {
                    SepetItem si = s.Urunler.FirstOrDefault(x => x.Urun.Id == id);
                    s.Urunler.Remove(si);
                }
            }
        }
        public PartialViewResult MiniSepetWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            }
            else
            {
                return PartialView();
            }
        }
        public ActionResult UrunDetay(string id)
        {
            Urun u = Context.Baglanti.Uruns.FirstOrDefault(x => x.Adi == id);
            List<UrunOzellik>
                uos = Context.Baglanti.UrunOzelliks.Where(x => x.UrunId == u.Id).ToList();

            Dictionary<string, List<OzellikDeger>> ozellik = new Dictionary<string, List<OzellikDeger>>();

            List<OzellikDeger> degers = new List<OzellikDeger>();

            foreach (UrunOzellik uo in uos)
            {
                OzellikTip ot = Context.Baglanti.OzellikTips.FirstOrDefault(x => x.Id == uo.OzellikTipId);

                bool feriha = false;
                foreach (var item in ozellik)
                {
                    if (item.Key != ot.Adi)
                    {
                        feriha = true;
                    }
                    else
                    {
                        feriha = false;
                    }
                }
                if (feriha)
                {
                    degers = new List<OzellikDeger>();
                }

                foreach (OzellikDeger deger in Context.Baglanti.OzellikDegers.Where(x => x.OzellikTipId == ot.Id).ToList())
                {
                    OzellikDeger od = Context.Baglanti.OzellikDegers.FirstOrDefault(x => x.OzellikTipId == ot.Id && x.Id == uo.OzellikDegerId);
                    if (!degers.Any(x => x.Id == od.Id))
                    {
                        degers.Add(od);
                    }
                }
                if (ozellik.Any(x => x.Key == ot.Adi))
                {
                    ozellik[ot.Adi] = degers;
                }
                else
                {
                    ozellik.Add(ot.Adi, degers);
                }
                
            }
            ViewBag.Ozellikler = ozellik;

            return View(u);
        }
        
	}
}