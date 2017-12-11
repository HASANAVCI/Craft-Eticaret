using CraftTicaret.WebUI.App_Classes;
using CraftTicaret.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CraftTicaret.WebUI.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler()
        {
            return View(Context.Baglanti.Uruns.ToList());
        }
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategoris.ToList();
            ViewBag.Markalar = Context.Baglanti.Markas.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun urn)
        {
            Context.Baglanti.Uruns.Add(urn);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Urunler");
        }
        public ActionResult Markalar()
        {
            return View(Context.Baglanti.Markas.ToList());
        }
        public ActionResult MarkaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MarkaEkle(Marka mrk, HttpPostedFileBase fileUpload)
        {
            int resimId = -1;
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                int width = Convert.ToInt32 (ConfigurationManager.AppSettings["MarkaWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaHeight"].ToString());
                string name = "/Content/MarkaResim/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName); 
                //resim çektiğimiz yer, resmin uzantısını alır.
                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));
                Resim rsm = new Resim(); //resim tanımladık
                rsm.OrtaYol = name;
                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();
                if (rsm.Id != null)
                    resimId = rsm.Id;
            }
            if (resimId != -1)
                mrk.ResimId = resimId;
            Context.Baglanti.Markas.Add(mrk);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");
        }
        public ActionResult Kategoriler()
        {
            return View(Context.Baglanti.Kategoris.ToList());
        }
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori ktg)
        {
            Context.Baglanti.Kategoris.Add(ktg);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Kategoriler");
        }
        public ActionResult OzellikTipleri()
        {
            return View(Context.Baglanti.OzellikTips.ToList());
        }
        public ActionResult OzellikTipEkle()
        {
            return View(Context.Baglanti.Kategoris.ToList());
        }
        [HttpPost]
        public ActionResult OzellikTipEkle(OzellikTip ot)
        {
            Context.Baglanti.OzellikTips.Add(ot); //ot=özellik tipi
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikTipleri");
        }
        public ActionResult OzellikDegerleri()
        {
            return View(Context.Baglanti.OzellikDegers.ToList());
        }
        public ActionResult OzellikDegerEkle()
        {
            return View(Context.Baglanti.OzellikTips.ToList());
        }
        [HttpPost]
        public ActionResult OzellikDegerEkle(OzellikDeger od)
        {
            Context.Baglanti.OzellikDegers.Add(od); //od=özellik değer
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikDegerleri");
        }
        public ActionResult UrunOzellikleri()
        {
            return View(Context.Baglanti.UrunOzelliks.ToList());
        }
        public ActionResult UrunOzellikSil(int urunId, int tipId, int degerId)
        {
            UrunOzellik uo = Context.Baglanti.UrunOzelliks.FirstOrDefault(x => x.UrunId == urunId && x.OzellikTipId == tipId && x.OzellikDegerId == degerId);
            Context.Baglanti.UrunOzelliks.Remove(uo);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }
        public ActionResult UrunOzellikEkle()
        {
            return View(Context.Baglanti.Uruns.ToList());
        }
        public PartialViewResult UrunOzellikTipWidget(int? katId) //int?=null olma ihtimaline karşı
        {
            if (katId!=null)
            {
                var data = Context.Baglanti.OzellikTips.Where(x => x.KategoriId == katId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Baglanti.OzellikTips.ToList();
                return PartialView(data);
            }
        }
        public PartialViewResult UrunOzellikDegerWidget(int? tipId)
        {
            if (tipId != null)
            {
                var data = Context.Baglanti.OzellikDegers.Where(x => x.OzellikTipId == tipId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Baglanti.OzellikDegers.ToList();
                return PartialView(data);
            }
        }
        [HttpPost]
        public ActionResult UrunOzellikEkle(UrunOzellik uo)
        {
            Context.Baglanti.UrunOzelliks.Add(uo);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }
        public ActionResult UrunResimEkle(int id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult UrunResimEkle(int uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap ortaResim = new Bitmap(img, Settings.UrunOrtaBoyut);
                Bitmap buyukResim = new Bitmap(img, Settings.UrunBuyukBoyut);
                string ortaYol = "/Content/UrunResim/Orta/" + Guid.NewGuid()+Path.GetExtension(fileUpload.FileName);
                string buyukYol = "/Content/UrunResim/Buyuk/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                ortaResim.Save(Server.MapPath(ortaYol));
                buyukResim.Save(Server.MapPath(buyukYol));
                Resim rsm = new Resim();
                rsm.BuyukYol = buyukYol;
                rsm.OrtaYol = ortaYol;
                rsm.UrunId = uId;
                if (Context.Baglanti.Resims.FirstOrDefault(x => x.UrunId == uId && x.Varsayilan == false) != null)
                    rsm.Varsayilan = true;
                else
                    rsm.Varsayilan = false;
                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();
                return View(uId);
            }
            return View(uId);
        }
        public ActionResult SliderResimleri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SliderResimEkle(HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap bmp = new Bitmap(img, Settings.SliderResimBoyut);
                string yol = "/Content/SliderResim" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                bmp.Save(Server.MapPath(yol));
                Resim rsm = new Resim();
                rsm.BuyukYol = yol;
                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();
            }
            return RedirectToAction("SliderResimleri");
        }
       
	}
}