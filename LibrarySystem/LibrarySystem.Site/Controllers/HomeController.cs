using Elmah;
using LibrarySystem.Dal;
using LibrarySystem.Helper;
using LibrarySystem.Service.Site.Service;
using LibrarySystem.Site.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static LibrarySystem.Helper.Tool;

namespace LibrarySystem.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BookModel _BookModel = new BookModel();

            try
            {
                _BookModel.CountryList = CountryList(); //Ulke listesini alıyorum
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//Elmah kullanarak olası hataları Db ye loglayacağım.
            }

            return View(_BookModel);//modek dönüyor
        }
        [HttpPost]
        public ActionResult Index(string x)
        {
            return View();
        }
        public static List<SelectListItem> CountryList()// Ulke listesini Servis katmanından al
        {
            BookService _BookService = new BookService();//instance aldım servisi
            List<SelectListItem> selectList = new List<SelectListItem> // list item hazırlıyorum
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "Seçiniz"
                }
            };
            List<Country> _Country = Tool.AsObjectList<Country>(_BookService.GetCountryList());
            foreach (Country item in _Country)// list item ekleme country için
            {
                selectList.Add(new SelectListItem()
                {
                    Value = item.ID.ToString(),
                    Text = item.Name.Trim()
                });
            }
            return selectList;
        }

        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Register(BookModel val)
        {
            string JsfunctionName = "CalculateReturn";// dönüşte çalıştırılacak funciton adı parametresi
            OptinalClass _OptinalClass = new OptinalClass();// genel kullanım class yapım
            OptinalClass rtrn = new OptinalClass();// genel kullanım class yapım
            BookService _BookService = new BookService();
            if (Convert.ToDateTime(val.BeginDate) > Convert.ToDateTime(val.EndDate))// false ise return dön
            {
                rtrn.OptinalClass_val = false;
                rtrn.OptinalClass_msg = "Başlangıç Tarihi, Bitiş Tarihinden Büyük Olamaz!";
                return Json(new { value = true, jqueryFunc = ("" + JsfunctionName + "({'value':'" + rtrn.OptinalClass_val + "','Return':'" + rtrn.OptinalClass_msg + "'})").Replace('\'', '"') });// code blok burada return döndürsün

            }
            if (val.Country == "0")//ülke gelen değer kontrol
            {
                rtrn.OptinalClass_val = false;
                rtrn.OptinalClass_msg = "Ülke Seçiniz!";
                return Json(new { value = true, jqueryFunc = ("" + JsfunctionName + "({'value':'" + rtrn.OptinalClass_val + "','Return':'" + rtrn.OptinalClass_msg + "'})").Replace('\'', '"') });// code blok burada return döndürsün
            }
            try
            {
                int.TryParse(val.Country, out int countryID);
                _OptinalClass.OptinalClass_ID = countryID;
                List<WorkDay> _WorkDayList = Tool.AsObjectList<WorkDay>(_BookService.GetWorkDayList(Tool.AsJson(_OptinalClass)));//Service projesinden, db de ülkeye göre tanımlanan çalışma günlerini alıyorum.
                List<Holiday> _Holiday = _BookService.GetHolidayList(Tool.AsJson(_OptinalClass));//Service projesinden, db de ülkeye göre tanımlanan tatil günlerini alıyorum.

                DateTime BeginDate = Convert.ToDateTime(val.BeginDate);
                DateTime EndDate = Convert.ToDateTime(val.EndDate);

                double TotalDay = (EndDate - BeginDate).TotalDays + 1;//dahil edilen gün sayısı

                int BookDay = 0; //çalışma gü sayısı
                int weekOfDay = -1; // gün, haftanın hangi indexinde(squence)
                bool isworkDay = false;// çalışma günü mü
                bool isholiday = false; // tatil günü mü
                float PenaltyPay = 0; //ceza ücreti
                string Curreny = ""; //para birimi
                Curreny = Tool.AsObjectList<Country>(_BookService.GetCountryList()).Where(x => x.ID == countryID).SingleOrDefault().Currency;
                for (int i = 0; i < TotalDay; i++) //para birimini alıyorum
                {
                    #region Gün sequnce belirleme
                    weekOfDay = Tool.GetDayOfWeek(BeginDate.AddDays(i));//helper daki tool da, tarih haftanın hangi gününde bilgisini alıyorum.
                    #endregion
                    isworkDay = false;
                    foreach (WorkDay workDay in _WorkDayList)//çalışma günü mü?
                    {
                        if (workDay.DaySequence == weekOfDay)//squence e göre bakıyorum.
                        {
                            isworkDay = true; // çalışma günü ise şarta girecek
                            break;
                        }
                    }
                    isholiday = false;
                    foreach (Holiday holiday in _Holiday)// tatile denk geliyor mu?
                    {
                        if (holiday.Date == BeginDate.AddDays(i))
                        {
                            isholiday = true;// tatilse şarta girmicek
                            break;
                        }
                    }
                    if (!isholiday && isworkDay)//tatil değilse ve çalışma günü ise
                    {
                        BookDay++;// günü 1 arttır.
                    }
                }
                if (BookDay > 10) // 10 çalışma gününden fazla ise ceza ücretine tabi olacak
                {
                    PenaltyPay = (BookDay - 10) * 5; //hergün 5 para birimi 
                }
                rtrn.OptinalClass_msg = BookDay.ToString(); // Çalışma gün sayısı
                rtrn.OptinalClass_optional_Msg = PenaltyPay.ToString() + " " + Curreny; // var ise Ceza tutarı
                rtrn.OptinalClass_val = true;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//Elmah kullanarak olası hataları Db ye loglayacağım.
            }
            return Json(new { value = true, jqueryFunc = ("" + JsfunctionName + "({'value':'" + rtrn.OptinalClass_val + "','WorkDays':'" + rtrn.OptinalClass_msg + "','Penalty':'" + rtrn.OptinalClass_optional_Msg + "'})").Replace('\'', '"') });// Json dönerken main.js te tanımladığım form submit fonksiyonu çalıştığından geriye gerekli parametreleri tanımlıyorum.
        }
    }
}