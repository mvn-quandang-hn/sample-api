using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.Application.DTOs;
using WebAPI.Application.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route($"{Constants.ApiPrefix}/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguagesController(ILanguageService LanguageService,IMapper mapper)
        {
            _languageService = LanguageService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<LanguageResponse>>> Create([FromBody] LanguageRequest LanguageRequest)
        {
            var Language = await _languageService.FindByName(LanguageRequest.Name);
            if (Language != null)
            {
                ModelState.AddModelError(nameof(LanguageRequest.Name), ErrorMessage.DuplicatedValue);
                return ValidationProblem(ModelState);
            }
            return Ok(new BaseResponse<LanguageResponse>
            {
                Data = await _languageService.Create(LanguageRequest)
            });

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<LanguageResponse>>> Remove([FromRoute] int id)
        {
            var Language = await _languageService.FindById(id);
            if (Language == null)
            {
                return NotFound();
            }
            return Ok(new BaseResponse<LanguageResponse>()
            {
                Data = await _languageService.Delete(Language)
            });
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<LanguageListResponse>>> List()
        {
            return Ok(new BaseResponse<LanguageListResponse>
            {
                Data = await _languageService.List()
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BaseResponse<LanguageResponse>>> EditForm([FromRoute] int id, [FromBody] LanguageRequest request)
        {
            var Language = await _languageService.FindById(id);
            if (Language == null)
            {
                return NotFound();
            }
            var existedName = await _languageService.FindByName(request.Name);
            if (existedName != null && existedName.Id != id)
            {
                ModelState.AddModelError(nameof(request.Name), ErrorMessage.DuplicatedValue);
                return ValidationProblem(ModelState);
            }
            return Ok(new BaseResponse<LanguageResponse>()
            {
                Data = await _languageService.EditLanguage(Language, request)
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<LanguageResponse>>> GetLanguageInfo([FromRoute] int id)
        {
            var Language = _mapper.Map<LanguageResponse>(await _languageService.FindById(id));
            return Language == null
              ? NotFound()
              : Ok(new BaseResponse<LanguageResponse>
              {
                  Data = Language
              });
        }
    }
}