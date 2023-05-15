using WebAPI.Application.Services;
using WebAPI.Application.Services.Interfaces;

namespace WebAPI.Extensions
{
    public static class WebAppBuilderExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILanguageService, LanguageService>();
        }
    }
}
