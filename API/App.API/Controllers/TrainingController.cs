using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
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
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly IMapper _mapper;

        public TrainingController(ITrainingService trainingService, IMapper mapper)
        {
            _trainingService = trainingService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<TrainingListItem>))]
        public async Task<ActionResult<PaginatedList<TrainingListItem>>> GetTrainings([FromQuery] TrainingQuery query)
        {
            var result = await _trainingService.GetTrainings(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingDto))]
        public async Task<ActionResult<TrainingDto>> GetTrainingById(Guid id)
        {
            var training = _mapper.Map<TrainingDto>(await _trainingService.GetByIdAsync(id));
            return Ok(training);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingsAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateTraining([FromBody] CreateTrainingVM model)
        {
            var training = _mapper.Map<Training>(model);
            await _trainingService.AddAsync(training);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateNews([FromBody] UpdateTrainingVM command)
        {
            await _trainingService.UpdateAsync(command);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingsActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> ActivateTraining(Guid id)
        {
            await _trainingService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingsDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateTraining(Guid id)
        {
            await _trainingService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingsDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _trainingService.RemoveAsync(id);
            return NoContent();
        }
    }
}
