using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.ServiceContracts.Parameter;
using Ilkyar.WebAPI.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace Ilkyar.WebAPI.Controllers
{
    public class ParameterController : ApiController
    {
        private readonly IParameter _parameterService;

        public ParameterController(IParameter parameterService)
        {
            _parameterService = parameterService;
        }

        [HttpGet]
        public IHttpActionResult GetCityList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetCityList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetTownList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetTownList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUniversityList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetUniversityList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetDepartmentList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetDepartmentList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectManagerList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetProjectManagerList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetScholarshipHolderList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetScholarshipHolderList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUserTypeList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetUserTypeList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetOccupationList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetOccupationList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetActivityList(string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetActivityList(Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetDashboardInfo()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetDashboardInfo();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetCity(string cityId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetCity(Convert.ToInt32(cityId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetTown(string cityId, string townId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetTown(Convert.ToInt32(cityId), Convert.ToInt32(townId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetInterestList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetInterestList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetVolunteerInterestList(string volunteerId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.GetVolunteerInterestList(Convert.ToInt64(volunteerId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UploadRequirementListFile(UploadRequirementListFileDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _parameterService.UploadRequirementListFile(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}