using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DTOs;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Common;

namespace WebAPI.Controllers;

[ApiController]
[Route($"{Constants.ApiPrefix}/[controller]")]
public class LanguagesController : ControllerBase
{
    private readonly ILanguageService _languageService;
    private readonly IMapper _mapper;

    public LanguagesController(ILanguageService languageService, IMapper mapper)
    {
        _languageService = languageService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<LanguageResponse>>> Create([FromBody] LanguageRequest languageRequest)
    {
        var language = await _languageService.FindByName(languageRequest.Name);
        if (language != null)
        {
            ModelState.AddModelError(nameof(languageRequest.Name), ErrorMessage.DuplicatedValue);
            return ValidationProblem(ModelState);
        }

        return Ok(new BaseResponse<LanguageResponse>
        {
            Data = await _languageService.Create(languageRequest)
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<LanguageResponse>>> Remove([FromRoute] int id)
    {
        var language = await _languageService.FindById(id);
        if (language == null) return NotFound();
        return Ok(new BaseResponse<LanguageResponse>
        {
            Data = await _languageService.Delete(language)
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
    public async Task<ActionResult<BaseResponse<LanguageResponse>>> Edit([FromRoute] int id,
        [FromBody] LanguageRequest request)
    {
        var language = await _languageService.FindById(id);
        if (language == null) return NotFound();
        var existedName = await _languageService.FindByName(request.Name);
        if (existedName != null && existedName.Id != id)
        {
            ModelState.AddModelError(nameof(request.Name), ErrorMessage.DuplicatedValue);
            return ValidationProblem(ModelState);
        }

        return Ok(new BaseResponse<LanguageResponse>
        {
            Data = await _languageService.EditLanguage(language, request)
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<LanguageResponse>>> GetLanguageInfo([FromRoute] int id)
    {
        var language = _mapper.Map<LanguageResponse>(await _languageService.FindById(id));
        return language == null
            ? NotFound()
            : Ok(new BaseResponse<LanguageResponse>
            {
                Data = language
            });
    }
}