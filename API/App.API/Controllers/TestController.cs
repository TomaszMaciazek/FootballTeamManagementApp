using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities.TestEntities;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.ServiceLayer.Models;
using App.ServiceLayer.Services;
using App.UserMiddleware;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestTemplateService _testTemplateService;
        private readonly IMapper _mapper;

        public TestController(ITestTemplateService TestTemplateService, IMapper mapper)
        {
            _testTemplateService = TestTemplateService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
        public async Task<ActionResult<TestTemplateDto>> GetTestById([FromRoute] Guid id)
        {
            var test = (await _testTemplateService.GetByIdAsync(id));
            return Ok(test);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TestQuestionDto>))]
        [Route("Questions/{id}")]
        public async Task<ActionResult<IEnumerable<TestQuestionDto>>> GetQuestionsFromTest([FromRoute] Guid id)
        {
            return Ok(await _testTemplateService.GetQuestionsFromTest(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FillTestDto))]
        [Route("FillTemplate/{id}")]
        public async Task<ActionResult<FillTestDto>> GetTestTemplateToFill([FromRoute] Guid id)
        {
            return Ok(await _testTemplateService.GetTestTemplateToFill(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsAllPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<TestListItemDto>))]
        [Route("All")]
        public async Task<ActionResult<PaginatedList<TestListItemDto>>> GetTests([FromQuery] TestTemplateQuery query)
        {
            return Ok(await _testTemplateService.GetTests(query));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsCreatedByMePolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MyTestListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CreatedByMe")]
        public async Task<ActionResult<PaginatedList<MyTestListItemDto>>> GetTestsFromAuthor([FromQuery] TestTemplateQuery query)
        {
            if (!query.AuthorId.HasValue)
            {
                return BadRequest();
            }
            return Ok(await _testTemplateService.GetTestsFromAuthor(query));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateTest([FromBody] CreateTestVM model)
        {
            var test = _mapper.Map<TestTemplate>(model);
            await _testTemplateService.AddAsync(test);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _testTemplateService.RemoveAsync(id);
            return NoContent();
        }
    }
}
