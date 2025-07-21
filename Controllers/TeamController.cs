using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        /// Method to creat new team
        [HttpPost("postCreateTeam")]
        public ActionResult insertTeamcon([FromBody] Team newTeam)
        {
            int temp = newTeam.addNewTeam(newTeam);
            if (temp == 1)
            {
                return Ok("Team added successfully");
            }
            return BadRequest("Error or existing Team");
        }


        [HttpGet("GetTeamList")]
        public List<Team> getTeamListCon()
        {
            return Team.ReadTeam(); 
          
        }



        [HttpPut("updateTeam")]
        public IActionResult UpdateTeam([FromBody] Team team)
        {
            Team updated = new Team().UpdateTeam(team);

            if (updated == null)
            {
                return Conflict(new { message = "Team not found or name already in use" });
            }

            return Ok(updated);
        }



        [HttpDelete("DeleteTeam")]
        public IActionResult DeleteTeam(string teamName)
        {
            Team team = new Team();
            bool result = team.DeleteTeam(teamName);

            if (result)
                return Ok(new { message = "Team deleted successfully." });
            else
                return StatusCode(500, new { message = "Failed to delete team." });
        }
    }

     
    }
