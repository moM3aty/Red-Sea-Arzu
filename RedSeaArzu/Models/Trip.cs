using System.ComponentModel.DataAnnotations;

namespace RedSeaArzu.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم بالإنجليزية مطلوب")]
        public string NameEn { get; set; }
        [Required(ErrorMessage = "الوصف بالإنجليزية مطلوب")]
        public string DescriptionEn { get; set; }

        [Required(ErrorMessage = "الاسم بالعربية مطلوب")]
        public string NameAr { get; set; }
        [Required(ErrorMessage = "الوصف بالعربية مطلوب")]
        public string DescriptionAr { get; set; }

        [Required(ErrorMessage = "الاسم بالألمانية مطلوب")]
        public string NameDe { get; set; }
        [Required(ErrorMessage = "الوصف بالألمانية مطلوب")]
        public string DescriptionDe { get; set; }

        [Required(ErrorMessage = "الاسم بالرومانية مطلوب")]
        public string NameRo { get; set; }
        [Required(ErrorMessage = "الوصف بالرومانية مطلوب")]
        public string DescriptionRo { get; set; }

        [Required(ErrorMessage = "سعر الرحلة مطلوب")]
        [Range(0.01, 10000.00, ErrorMessage = "الرجاء إدخال سعر صالح")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
