namespace WebAPI.Application.Models.ViewModel
{
    public class LanguageViewModel
    {
        public int? Id { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public byte? IsDefault { get; set; }
        public byte? IsRequired { get; set; }
        public string? SavedBy { get; set; }
    }
}
