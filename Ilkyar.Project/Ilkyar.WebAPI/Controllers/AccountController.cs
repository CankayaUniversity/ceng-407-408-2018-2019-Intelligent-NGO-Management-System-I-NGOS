using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.ServiceContracts.Account;
using Ilkyar.WebAPI.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace Ilkyar.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccount _accountService;

        public AccountController(IAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IHttpActionResult Login(string username, string password)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.Login(username, password);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewVolunteer(CreateNewVolunteerDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewVolunteer(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewDonator(CreateNewDonatorDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewDonator(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewNGOHead(CreateNewNGOHeadDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewNGOHead(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewProjectManager(CreateNewProjectManagerDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewProjectManager(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewScholarshipCommittee(CreateNewScholarshipCommitteeDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewScholarshipCommittee(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewYonDer(CreateNewYonderDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewYonder(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewHostSchoolTeacher(CreateNewHostSchoolTeacherDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewHostSchoolTeacher(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewStudent(CreateNewStudentDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.CreateNewStudent(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetVolunteerBadge(string volunteerId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.GetVolunteerBadge(Convert.ToInt64(volunteerId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectManagerBadge(string projectManagerId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _accountService.GetProjectManagerBadge(Convert.ToInt64(projectManagerId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}