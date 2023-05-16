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
        public LanguageService(MasterDbContext masterDbContex, IMapper mapper)
        {
            _masterDbContext = masterDbContex;
            _mapper = mapper;
        }
        public async Task<LanguageResponse> Create(LanguageRequest createLanguageRequest)
        {
            var Language = _mapper.Map<Language>(createLanguageRequest);
            _masterDbContext.Add(Language);
            await _masterDbContext.SaveChangesAsync();
            return _mapper.Map<LanguageResponse>(Language);
        }

        public async Task<LanguageResponse> Delete(Language Language)
        {
            _masterDbContext.Remove(Language);
            await _masterDbContext.SaveChangesAsync();
            return _mapper.Map<LanguageResponse>(Language);
        }

        public async Task<Language?> FindById(int? id)
        {
            return await _masterDbContext.Languages.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<LanguageListResponse> List()
        {
            var Languages = await _masterDbContext.Languages.OrderByDescending(c => c.Id).ToListAsync();
            return new LanguageListResponse
            {
                Languages = _mapper.Map<List<LanguageViewModel>>(Languages),
            };
        }

        public Task<Language?> FindByName(string name)
        {
            return _masterDbContext.Languages.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<LanguageResponse> EditLanguage(Language Language, LanguageRequest request)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                Language.Locale = request.Locale;
                Language.Name = request.Name;
                Language.SavedBy = request.SavedBy;
                _masterDbContext.Entry(Language).State = EntityState.Modified;
                await _masterDbContext.SaveChangesAsync();
            }
            return _mapper.Map<LanguageResponse>(Language);
        }

        public async Task<Language?> FindById(int id)
        {
            return await _masterDbContext.Languages.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
