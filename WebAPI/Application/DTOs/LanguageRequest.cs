using System.ComponentModel.DataAnnotations;

namespace WebAPI.Application.DTOs
{
    public class LanguageRequest
    {
        [Required, MaxLength(10)]
        public string Locale { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public byte? IsDefault { get; set; }
        public byte? IsRequired { get; set; }
        [MaxLength(255)]
        public string? SavedBy { get; set; }
    }
}
