using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.ServiceContracts.User;
using Ilkyar.WebAPI.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace Ilkyar.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IHttpActionResult GetUserList(UserFilterDTO filter)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _userService.GetUserList(filter);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUserList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _userService.GetUserList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUser(string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _userService.GetUser(Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}