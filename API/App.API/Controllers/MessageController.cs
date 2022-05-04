using App.DataAccess.Exceptions;
using App.Model.Dtos;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.ServiceLayer.Models;
using App.ServiceLayer.Services;
using App.UserMiddleware;
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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<ActionResult<MessageDto>> GetMessageTransmissionById([FromRoute] Guid id)
        {
            var transmission = await _messageService.GetMessageTransmissionByIdAsync(id);
            if (transmission == null)
            {
                return NotFound();
            }
            return Ok(transmission);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MessageDto>))]
        public async Task<ActionResult<PaginatedList<MessageDto>>> GetMessagesFromUser([FromQuery] MessageQuery query)
        {
            var messages = await _messageService.GetMessagesFromUserAsync(query);
            return Ok(messages);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Unread/{id}")]
        public async Task<ActionResult<int>> GetNumberOfUnreadMessages([FromRoute] Guid id)
        {
            try
            {
                var count = await _messageService.GetNumberOfUnreadMessages(id);
                return Ok(count);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageVM model)
        {
            try
            {
                await _messageService.CreateMessageAsync(model);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageVM model)
        {
            try
            {
                await _messageService.UpdateMessageAsync(model);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("TakeFromTrash")]
        public async Task<ActionResult<bool>> TakeMessagesFromTrash([FromBody] SelectedMessagesVM model)
        {
            await _messageService.TakeMessagesFromTrash(model.UserId, model.MessagesIds);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Status")]
        public async Task<IActionResult> ChangeMessagesStatus([FromBody] ChangeMessagesStatusVM model)
        {
            await _messageService.ChangeMessagesTransmissionsStatus(model);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MessagesPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMessages([FromBody] SelectedMessagesVM model)
        {
            await _messageService.RemoveUserMessagesAsync(model.UserId,model.MessagesIds);
            return NoContent();
        }
    }
}
