using FCManager.DataAccess.Interfaces;
using FCManager.Models.ViewModels.Team;
using FCManager.Models.ViewModels.TeamMembers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FCManager.Models.Wrappers.Enum;

namespace FCManager_Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IApiRepository _apiRepo;

        public ApiController(IApiRepository apiRepo)
        {
            _apiRepo = apiRepo;
        }

        [HttpGet("ViewTeam/{id}")]
        public async Task<IActionResult> ViewTeamAsync([FromRoute] string id)
        {
            var result = await _apiRepo.ViewTeamAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("ViewTeamMember/{id}")]
        public async Task<IActionResult> ViewTeamMemberAsync([FromRoute] string id)
        {
            var result = await _apiRepo.ViewTeamMemberAsync(id);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("ViewTeamList/{page}/{pageSize}")]
        public async Task<IActionResult> ViewTeamList([FromRoute] int page, int pageSize)
        {
            var result = await _apiRepo.ViewTeamList(page, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("ViewTeamMemberList/{page}/{pageSize}/{teamId}")]
        public async Task<IActionResult> ViewTeamMemberList([FromRoute] int page, int pageSize, string teamId)
        {
            var result = await _apiRepo.ViewTeamMemberList(page, pageSize, teamId);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("CreateTeam")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamViewModel createTeamViewModel)
        {
            var result = await _apiRepo.CreateTeam(createTeamViewModel);
            return StatusCode(result.StatusCode, result);
        }

        //GET IMAGE
        [HttpGet("GetImage/{imageType}/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] string id, ImageType imageType)
        {
            var result = await _apiRepo.GetImage(id, imageType);
            return StatusCode(result.StatusCode, result);
        }

        ////DELETE TEAM
        [Authorize(Roles = "GlobalAdmin")]
        [HttpDelete("DeleteTeam/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _apiRepo.Delete(id);
            return StatusCode(result.StatusCode, result);
        }

        ////DELETE TEAM MEMBER
        [Authorize(Roles = "GlobalAdmin")]
        [HttpDelete("DeleteTeamMember/{id}")]
        public async Task<IActionResult> DeleteTeamMember([FromRoute] string id)
        {
            var result = await _apiRepo.DeleteTeamMember(id);
            return StatusCode(result.StatusCode, result);
        }

        ////UPDATE TEAM
        [Authorize]
        [HttpPut("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam([FromBody] TeamViewModel request)
        {
            var result = await _apiRepo.UpdateTeam(request);
            return StatusCode(result.StatusCode, result);
        }

        ////UPDATE TEAM MEMBER
        [Authorize]
        [HttpPut("UpdateTeamMember")]
        public async Task<IActionResult> UpdateTeamMember([FromBody] UpdateTeamMemberViewModel request)
        {
            var result = await _apiRepo.UpdateTeamMember(request);
            return StatusCode(result.StatusCode, result);
        }

        #region
        [Authorize]
        [HttpPost("AddTeamMember")]
        public async Task<IActionResult> AddTeamMember([FromBody] CreateTeamMemberViewModel request)
        {
            var result = await _apiRepo.AddTeamMember(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateTeamMemberViewModel request)
        {
            var result = await _apiRepo.Register(request);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
