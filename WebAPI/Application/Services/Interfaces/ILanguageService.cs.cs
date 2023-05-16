using WebAPI.Application.Models;
using WebAPI.Application.DTOs;
namespace WebAPI.Application.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<LanguageResponse> Create(LanguageRequest createLanguageRequest);
        Task<Language?> FindByName(string name);
        Task<LanguageResponse> Delete(Language Language);
        Task<Language?> FindById(int? id);
        Task<LanguageListResponse> List();
        Task<LanguageResponse> EditLanguage(Language Language, LanguageRequest request);
    }
}
