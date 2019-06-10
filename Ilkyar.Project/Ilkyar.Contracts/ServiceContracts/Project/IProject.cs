using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;
using System.Collections.Generic;

namespace Ilkyar.Contracts.ServiceContracts.Project
{
    public interface IProject
    {
        ServiceResult<long> CreateNewProject(CreateNewProjectDTO model);
        ServiceResult<long> CreateProjectDetail(CreateProjectDetailDTO model);
        ServiceResult<List<ProjectDTO>> GetProjectList(ProjectFilterDTO filter);
        ServiceResult<ProjectDTO> GetProject(long projectId);
        ServiceResult<bool> UpdateProject(UpdateProjectDTO model);
        ServiceResult<List<ProjectSubDetailDTO>> GetProjectSubDetailList(long projectId);
        ServiceResult<bool> UpdateProjectSubDetail(UpdateProjectSubDetailDTO model);
        ServiceResult<ProjectSubDetailDTO> GetProjectSubDetail(long projectId, long projectDetailId);
        ServiceResult<ProjectDetailSummaryDTO> GetProjectDetailSummary(long projectId, long projectDetailId);
        ServiceResult<List<ParticipantDTO>> GetParticipantList(long projectId, long projectDetailId);
        ServiceResult<long> AddNewParticipant(AddParticipantDTO model);
        ServiceResult<bool> DeleteParticipant(DeleteParticipantDTO model);
        ServiceResult<List<ProjectDetailActivityDTO>> GetProjectDetailActivityList(ProjectDetailActivityFilterDTO model);
        ServiceResult<long> AddNewProjectDetailActivity(AddProjectDetailActivityDTO model);
        ServiceResult<bool> DeleteProjectDetailActivity(DeleteProjectDetailActivityDTO model);
        ServiceResult<bool> UpdateProjectDetailActivity(UpdateProjectDetailActivityDTO model);
        ServiceResult<long> CreateNGOInvitation(CreateNGOInvitationDTO model);
        ServiceResult<List<VoteProjectProjectDetailDTO>> GetVoteProjectProjectDetailList(long userId);
        ServiceResult<List<SurveyProjectDetailQuestionDTO>> GetSurveyProjectDetailQuestionList(long projectDetailId, long userId);
        ServiceResult<long> AddNewSurveyProjectDetailQuestion(AddSurveyProjectDetailQuestionDTO model);
        ServiceResult<List<VoteVolunteerProjectDetailDTO>> GetVoteVolunteerProjectDetailList(long userId);
        ServiceResult<List<EvaluateVolunteerDTO>> GetEvaluateVolunteerList(long projectDetailId, long userId);
        ServiceResult<long> AddNewVolunteerVote(AddVolunteerVoteDTO model);
        ServiceResult<List<LeadershipBoardDTO>> GetLeadershipBoardList();
        ServiceResult<List<ProjectDetailActivityScheduleDTO>> GetProjectDetailScheduleList(long projectDetailId);
        ServiceResult<bool> UpdateProjectDetailActivitySchedule(UpdateProjectDetailActivityScheduleDTO model);
        ServiceResult<List<ProjectScheduleProjectDetailDTO>> GetProjectScheduleProjectDetailList(long userId);
        ServiceResult<List<NGOInvitationDTO>> GetNGOInvitationList();
        ServiceResult<bool> UpdateNGOInvitation(UpdateNGOInvitationDTO model);
        ServiceResult<List<SurveyProjectDetailResultDTO>> GetSurveyProjectDetailResultList(long projectId, long projectDetailId);
        ServiceResult<List<ActivityDTO>> GetActivityList();
        ServiceResult<long> AddNewProjectActivity(AddProjectActivityDTO model);
        ServiceResult<List<ProjectActivityDTO>> GetProjectActivityList(long projectId, long projectDetailId);
        ServiceResult<List<ProjectDetailSuggestionDTO>> GetProjectDetailSuggestionList(long volunteerId);
        ServiceResult<bool> UpdateProjectActivity(UpdateProjectActivityDTO model);
        ServiceResult<VolunteerSuggestionDTO> GetVolunteerSuggestion(VolunteerSuggestionFilterDTO model);
    }
}
