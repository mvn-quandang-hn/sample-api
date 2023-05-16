using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Models;
using WebAPI.Application.DTOs;
using WebAPI.Application.Models.ViewModel;
using WebAPI.Application.Services.Interfaces;

namespace WebAPI.Application.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly MasterDbContext _masterDbContext;
        private readonly IMapper _mapper;
        public LanguageService(MasterDbContext masterDbContext, IMapper mapper)
        {
            _masterDbContext = masterDbContext;
            _mapper = mapper;
        }
        public async Task<LanguageResponse> Create(LanguageRequest createLanguageRequest)
        {
            var language = _mapper.Map<Language>(createLanguageRequest);
            _masterDbContext.Add(language);
            await _masterDbContext.SaveChangesAsync();
            return _mapper.Map<LanguageResponse>(language);
        }

        public async Task<LanguageResponse> Delete(Language language)
        {
            _masterDbContext.Remove(language);
            await _masterDbContext.SaveChangesAsync();
            return _mapper.Map<LanguageResponse>(language);
        }

        public async Task<Language?> FindById(int? id)
        {
            return await _masterDbContext.Languages.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<LanguageListResponse> List()
        {
            var languages = await _masterDbContext.Languages.OrderByDescending(c => c.Id).ToListAsync();
            return new LanguageListResponse
            {
                Languages = _mapper.Map<List<LanguageViewModel>>(languages)
            };
        }

        public Task<Language?> FindByName(string name)
        {
            return _masterDbContext.Languages.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<LanguageResponse> EditLanguage(Language language, LanguageRequest request)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                language.Locale = request.Locale;
                language.Name = request.Name;
                language.SavedBy = request.SavedBy;
                _masterDbContext.Entry(language).State = EntityState.Modified;
                await _masterDbContext.SaveChangesAsync();
            }
            return _mapper.Map<LanguageResponse>(language);
        }

        public async Task<Language?> FindById(int id)
        {
            return await _masterDbContext.Languages.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
