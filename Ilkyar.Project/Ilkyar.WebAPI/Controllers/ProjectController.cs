using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.ServiceContracts.Project;
using Ilkyar.WebAPI.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace Ilkyar.WebAPI.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProject _projectService;

        public ProjectController(IProject projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public IHttpActionResult CreateNewProject(CreateNewProjectDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.CreateNewProject(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult GetProjectList(ProjectFilterDTO filter)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectList(filter);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateProjectDetail(CreateProjectDetailDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.CreateProjectDetail(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProject(string projectId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProject(Convert.ToInt64(projectId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProject(UpdateProjectDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.UpdateProject(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectSubDetailList(string projectId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectSubDetailList(Convert.ToInt64(projectId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProjectSubDetail(UpdateProjectSubDetailDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.UpdateProjectSubDetail(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectSubDetail(string projectId, string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectSubDetail(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectDetailSummary(string projectId, string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectDetailSummary(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetParticipantList(string projectId, string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetParticipantList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewParticipant(AddParticipantDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.AddNewParticipant(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteParticipant(DeleteParticipantDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.DeleteParticipant(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult GetProjectDetailActivityList(ProjectDetailActivityFilterDTO filter)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectDetailActivityList(filter);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewProjectDetailActivity(AddProjectDetailActivityDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.AddNewProjectDetailActivity(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteProjectDetailActivity(DeleteProjectDetailActivityDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.DeleteProjectDetailActivity(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProjectDetailActivity(UpdateProjectDetailActivityDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.UpdateProjectDetailActivity(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNGOInvitation(CreateNGOInvitationDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.CreateNGOInvitation(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetVoteProjectProjectDetailList(string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetVoteProjectProjectDetailList(Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetSurveyProjectDetailQuestionList(string projectDetailId, string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetSurveyProjectDetailQuestionList(Convert.ToInt64(projectDetailId), Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewSurveyProjectDetailQuestion(AddSurveyProjectDetailQuestionDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.AddNewSurveyProjectDetailQuestion(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetVoteVolunteerProjectDetailList(string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetVoteVolunteerProjectDetailList(Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetEvaluateVolunteerList(string projectDetailId, string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetEvaluateVolunteerList(Convert.ToInt64(projectDetailId), Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewVolunteerVote(AddVolunteerVoteDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.AddNewVolunteerVote(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetLeadershipBoardList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetLeadershipBoardList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectDetailScheduleList(string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectDetailScheduleList(Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProjectDetailActivitySchedule(UpdateProjectDetailActivityScheduleDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.UpdateProjectDetailActivitySchedule(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectScheduleProjectDetailList(string userId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectScheduleProjectDetailList(Convert.ToInt64(userId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetNGOInvitationList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetNGOInvitationList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateNGOInvitation(UpdateNGOInvitationDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.UpdateNGOInvitation(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetSurveyProjectDetailResultList(string projectId,string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetSurveyProjectDetailResultList(Convert.ToInt64(projectId),Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetActivityList()
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetActivityList();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewProjectActivity(AddProjectActivityDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.AddNewProjectActivity(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectActivityList(string projectId, string projectDetailId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectActivityList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectDetailSuggestionList(string volunteerId)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetProjectDetailSuggestionList(Convert.ToInt64(volunteerId));
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProjectActivity(UpdateProjectActivityDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.UpdateProjectActivity(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult GetVolunteerSuggestion(VolunteerSuggestionFilterDTO model)
        {
            if (!Request.Headers.Contains("apiKey"))
                return Unauthorized();

            string apiKey = Request.Headers.GetValues("apiKey").First();

            if (!ApiHelper.CheckKey(apiKey))
                return Unauthorized();

            try
            {
                var serviceResult = _projectService.GetVolunteerSuggestion(model);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

