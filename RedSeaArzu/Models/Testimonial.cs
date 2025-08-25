using System.ComponentModel.DataAnnotations;

namespace RedSeaArzu.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم العميل مطلوب")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "بلد العميل مطلوبة")]
        [StringLength(100)]
        public string Country { get; set; }

        [Required(ErrorMessage = "الرسالة مطلوبة")]
        public string Message { get; set; }

        public bool IsApproved { get; set; } = false;
    }
}