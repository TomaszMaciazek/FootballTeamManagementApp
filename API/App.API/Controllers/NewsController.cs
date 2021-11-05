using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.ServiceLayer.Models;
using App.ServiceLayer.Queries;
using App.ServiceLayer.Services;
using App.UserMiddleware;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<NewsDto>))]
        public async Task<ActionResult<PaginatedList<NewsDto>>> GetNews([FromQuery] NewsQuery query)
        {
            return Ok(_mapper.Map<PaginatedList<NewsDto>>(await _newsService.GetNews(query)));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.NewsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsDto))]
        public async Task<ActionResult<NewsDto>> GetNewsById(Guid id)
        {
            var news = _mapper.Map<TranslationDto>(await _newsService.GetByIdAsync(id));
            return Ok(news);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.NewsAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateNews([FromBody] NewsDto model)
        {
            var news = _mapper.Map<News>(model);
            await _newsService.AddAsync(news);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.NewsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateNews([FromBody] UpdateNewsCommand model)
        {
            await _newsService.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.NewsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> ActivateNews(Guid id)
        {
            await _newsService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.NewsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateNews(Guid id)
        {
            await _newsService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.NewsDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _newsService.RemoveAsync(id);
            return NoContent();
        }
    }
}
