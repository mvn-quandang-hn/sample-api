using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Application.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Languages : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
