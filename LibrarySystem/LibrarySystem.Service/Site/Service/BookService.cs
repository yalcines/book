using Elmah;
using LibrarySystem.Dal;
using LibrarySystem.Helper;
using LibrarySystem.Service.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using static LibrarySystem.Helper.Tool;

namespace LibrarySystem.Service.Site.Service
{
    public class BookService : BaseService
    {
        public string GetCountryList()
        {
            List<Country> _CountryList = new List<Country>();
            try
            {
                _CountryList = db.Country.OrderBy(x => x.ID).ToList();// LİNQ ile Entity kullanarak List tipinde ülkeleri alıyorum
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//Elmah kullanarak olası hataları Db ye loglayacağım.
            }
            return Tool.AsJsonList(_CountryList);//Servis katmanında gelen ve giden datalar sürekli Json olacak
        }
        public string GetWorkDayList(string val)
        {
            OptinalClass _OptinalClass = new OptinalClass();
            _OptinalClass = Tool.AsObject<OptinalClass>(val);//genel class yapısına Deserialize
            List<WorkDay> _WorkDayList = new List<WorkDay>();
            try
            {
                _WorkDayList = db.WorkDay.Where(x => x.CountryID == _OptinalClass.OptinalClass_ID).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//Elmah kullanarak olası hataları Db ye loglayacağım.
            }

            return Tool.AsJsonList(_WorkDayList);//Servis katmanında gelen ve giden datalar sürekli Json olacak
        }
        public List<Holiday> GetHolidayList(string val)
        {
            OptinalClass _OptinalClass = new OptinalClass();
            _OptinalClass = Tool.AsObject<OptinalClass>(val);//genel class yapısına Deserialize
            List<Holiday> _HolidayList = new List<Holiday>();
            try
            {
                _HolidayList = db.Holiday.Where(x => x.CountryID == _OptinalClass.OptinalClass_ID).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//Elmah kullanarak olası hataları Db ye loglayacağım.
            }

            return _HolidayList;
        }

    }
}
