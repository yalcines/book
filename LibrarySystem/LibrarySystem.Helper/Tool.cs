using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace LibrarySystem.Helper
{
    public static class Tool
    {
        #region JsonConverts DATA STANDARTI SUREKLI JSON OLARAK SAĞLANACAK

        public static string AsJsonList<T>(List<T> tt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 2147483647
            };
            return serializer.Serialize(tt).Replace("\"\\/Date(", "").Replace(")\\/\"", "");
        }
        public static string AsJson<T>(T t)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 2147483647
            };
            return serializer.Serialize(t).Replace("\"\\/Date(", "").Replace(")\\/\"", "");
        }
        public static List<T> AsObjectList<T>(string tt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 2147483647
            };
            return serializer.Deserialize<List<T>>(tt);
        }
        public static T AsObject<T>(string t)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 2147483647
            };
            return serializer.Deserialize<T>(t);
        }
        #endregion
        #region GENEL RETURN CLASSIM
        public class OptinalClass
        {
            public string OptinalClass_msg { get; set; }
            public bool OptinalClass_val { get; set; }
            public bool OptinalClass_optional_Val { get; set; }
            public string OptinalClass_optional_Msg { get; set; }
            public int OptinalClass_ID { get; set; }
            public string OptinalClass_IDs { get; set; }
            public int OptinalClass_optional_ID { get; set; }
            public string OptinalClass_optional_IDs { get; set; }
        }
        #endregion
        #region Genel Helper
        public static int GetDayOfWeek(DateTime val) // gelen tarih değerini, haftanın gün olarak kaçıncı index sayısında olduğunu veriyor.
        {
            int weekOfDay = -1;
            switch (val.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    weekOfDay = 0;
                    break;
                case DayOfWeek.Monday:
                    weekOfDay = 1;
                    break;
                case DayOfWeek.Tuesday:
                    weekOfDay = 2;
                    break;
                case DayOfWeek.Wednesday:
                    weekOfDay = 3;
                    break;
                case DayOfWeek.Thursday:
                    weekOfDay = 4;
                    break;
                case DayOfWeek.Friday:
                    weekOfDay = 5;
                    break;
                case DayOfWeek.Saturday:
                    weekOfDay = 6;
                    break;
                default:
                    weekOfDay = -1;
                    break;
            }
            return weekOfDay;
        } 
        #endregion
    }
}
