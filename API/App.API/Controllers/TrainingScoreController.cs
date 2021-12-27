using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using App.ServiceLayer.Services;
using App.UserMiddleware;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingScoreController : ControllerBase
    {
        private readonly ITrainingScoreService _trainingScoreService;
        private readonly IMapper _mapper;

        public TrainingScoreController(ITrainingScoreService trainingScoreService, IMapper mapper)
        {
            _trainingScoreService = trainingScoreService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<TrainingScoreListItemDto>))]
        public async Task<ActionResult<PaginatedList<TrainingScoreListItemDto>>> GetNews([FromQuery] TrainingScoreQuery query)
        {
            var result = await _trainingScoreService.GetScores(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingScoreDto))]
        public async Task<ActionResult<TrainingScoreDto>> GetScoreById(Guid id)
        {
            var score = _mapper.Map<TrainingScoreDto>(await _trainingScoreService.GetByIdAsync(id));
            return Ok(score);
        }

        [HttpGet("Training/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleTrainingScoreDto>))]
        public async Task<ActionResult<IEnumerable<SimpleTrainingScoreDto>>> GetTrainingScoresFromTraining(Guid id)
        {
            return Ok(await _trainingScoreService.GetTrainingScoresFromTraining(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateTrainingsScore([FromBody] CreateTrainingScoreVM model)
        {
            var trainingScore = _mapper.Map<TrainingScore>(model);
            await _trainingScoreService.AddAsync(trainingScore);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTrainingScore([FromBody] UpdateTrainingScoreVM model)
        {
            await _trainingScoreService.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> ActivateTrainingScore(Guid id)
        {
            await _trainingScoreService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateTrainingScore(Guid id)
        {
            await _trainingScoreService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _trainingScoreService.RemoveAsync(id);
            return NoContent();
        }
    }
}
