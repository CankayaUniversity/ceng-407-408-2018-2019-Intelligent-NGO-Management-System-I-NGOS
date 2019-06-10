using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.ServiceContracts.Message;
using Ilkyar.WebAPI.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace Ilkyar.WebAPI.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessage _messageService;

        public MessageController(IMessage messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IHttpActionResult GetConversationList(string currentUserId, string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _messageService.GetConversationList(Convert.ToInt64(currentUserId), Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewConversation(AddConversationDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _messageService.CreateNewConversation(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteConversation(DeleteConversationDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _messageService.DeleteConversation(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}