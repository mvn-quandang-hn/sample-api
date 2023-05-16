using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Application.DTOs;
using WebAPI.Application.Models;
using WebAPI.Application.Models.ViewModel;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Controllers;
using WebAPITest.Common;

namespace WebAPITest.Controllers;

public class LanguagesControllerTest
{
    private readonly LanguagesController _controller;
    private readonly Mock<ILanguageService> _languageService;

    public LanguagesControllerTest()
    {
        _languageService = new Mock<ILanguageService>();
        Mock<IMapper> mapper = new();
        _controller = new LanguagesController(
            _languageService.Object, mapper.Object
        );
    }

    [Fact]
    public async void EditLanguageSuccess()
    {
        var languageId = 1;
        var testCase = new TestCase<LanguageRequest, LanguageResponse>
        {
            Input = new LanguageRequest
            {
                Locale = "en-us",
                Name = "English",
                SavedBy = "Tester"
            },
            ExpectedResult = new LanguageResponse
            {
                Locale = "en-us",
                Name = "English",
                SavedBy = "Tester"
            }
        };
        var mLanguage = new Language
        {
            Locale = "en-us",
            Name = "English",
            SavedBy = "Tester"
        };
        _languageService.Setup(s => s.FindById(It.IsAny<int>())).ReturnsAsync(mLanguage);
        _languageService.Setup(s => s.EditLanguage(mLanguage, testCase.Input)).ReturnsAsync(testCase.ExpectedResult);
        var actionResult = await _controller.Edit(languageId, testCase.Input);
        var result = actionResult.Result as OkObjectResult;
        var adLanguage = result?.Value as BaseResponse<LanguageResponse>;
        Assert.NotNull(result);
        Assert.Equal(testCase.ExpectedResult.Name, adLanguage?.Data.Name);
    }

    [Fact]
    public async void GetLanguagesSuccess()
    {
        var languagesResponse = new LanguageListResponse
        {
            Languages = new List<LanguageViewModel>
            {
                new()
                {
                    Id = 1,
                    Locale = "en-us",
                    Name = "English"
                }
            }
        };
        _languageService.Setup(c => c.List()).ReturnsAsync(languagesResponse);
        var languages = await _controller.List();
        var result = languages.Result as OkObjectResult;
        var response = result?.Value as BaseResponse<LanguageListResponse>;
        Assert.Equal(languagesResponse.Languages[0].Id, response?.Data.Languages[0].Id);
    }
}