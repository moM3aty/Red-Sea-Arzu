using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSeaArzu.Models
{
    public class Trip
    {
        public int Id { get; set; }

        // English
        [Required(ErrorMessage = "الاسم بالإنجليزية مطلوب")]
        public string NameEn { get; set; }
        [Required(ErrorMessage = "الوصف بالإنجليزية مطلوب")]
        public string DescriptionEn { get; set; }
        public string? LocationEn { get; set; }
        public string? IncludesEn { get; set; }
        public string? ExcludesEn { get; set; }

        // Arabic
        [Required(ErrorMessage = "الاسم بالعربية مطلوب")]
        public string NameAr { get; set; }
        [Required(ErrorMessage = "الوصف بالعربية مطلوب")]
        public string DescriptionAr { get; set; }
        public string? LocationAr { get; set; }
        public string? IncludesAr { get; set; }
        public string? ExcludesAr { get; set; }

        // German
        [Required(ErrorMessage = "الاسم بالألمانية مطلوب")]
        public string NameDe { get; set; }
        [Required(ErrorMessage = "الوصف بالألمانية مطلوب")]
        public string DescriptionDe { get; set; }
        public string? LocationDe { get; set; }
        public string? IncludesDe { get; set; }
        public string? ExcludesDe { get; set; }

        // Romanian
        [Required(ErrorMessage = "الاسم بالرومانية مطلوب")]
        public string NameRo { get; set; }
        [Required(ErrorMessage = "الوصف بالرومانية مطلوب")]
        public string DescriptionRo { get; set; }
        public string? LocationRo { get; set; }
        public string? IncludesRo { get; set; }
        public string? ExcludesRo { get; set; }

        // Italian (New)
        [Required(ErrorMessage = "الاسم بالإيطالية مطلوب")]
        public string NameIt { get; set; }
        [Required(ErrorMessage = "الوصف بالإيطالية مطلوب")]
        public string DescriptionIt { get; set; }
        public string? LocationIt { get; set; }
        public string? IncludesIt { get; set; }
        public string? ExcludesIt { get; set; }

        // Shared Trip Details
        [Required(ErrorMessage = "سعر الرحلة مطلوب")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 10000.00, ErrorMessage = "الرجاء إدخال سعر صالح")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PriceBefore { get; set; } // New: Price before discount

        [Required(ErrorMessage = "مدة الرحلة بالساعات مطلوبة")]
        public int DurationHours { get; set; } // New: Duration in hours

        public string? ImageUrl { get; set; }
    }
}
