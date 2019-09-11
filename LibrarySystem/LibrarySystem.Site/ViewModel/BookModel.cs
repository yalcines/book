using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LibrarySystem.Site.ViewModel
{
    public class BookModel //Kayıt ekranında kullanacağım model form elementleri
    {
        
        [Required(ErrorMessage = "'{0}' alanı boş olamaz.")]
        [Display(Name = "Kitap Alım Tarihi")]
        public string BeginDate { get; set; }

        [Required(ErrorMessage = "'{0}' alanı boş olamaz.")]
        [Display(Name = "Kitap Teslim Tarihi")]
        public string EndDate { get; set; } 

        [Required(ErrorMessage = "'{0}' alanı boş olamaz.")]
        [Display(Name = "Ülke")]
        public string Country { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> CountryList = new List<SelectListItem>();
    }
}