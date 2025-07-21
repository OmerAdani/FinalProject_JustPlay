using Final_project1.BL;
using Final_project1.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : Controller
    {
        [HttpGet("GetCoachesList")]
        public IEnumerable<Coach> GetRead()
        {
            return Coach.ReadCoach();
        }

        /// Method to creat new coach
        [HttpPost("postCreatCoach")]
        public ActionResult insertCoachcon([FromBody] Coach newCoach)
        {
            int temp = newCoach.Register_Coach(newCoach);
            if (temp == 1)
            {
                return Ok(new { message = "Coach added successfully" });
            }
            return BadRequest("Error or existing Coach");
        }


        [HttpPut("updateCoach")]
        public IActionResult updateCoachcon([FromBody] Coach Coach)
        {
            Coach updatedCoach = new Coach();
            Coach temp = updatedCoach.UpdateCoach(Coach);

            if (temp == null)
            {
                return Conflict(new { message = "User not found or email is already in use" });
            }

            return Ok(Coach);
        }


    


        [HttpDelete("deleteCoach/{coachId}")]
        public IActionResult DeleteCoach(int coachId)
        {
            DBservices_delete dbs_delete = new DBservices_delete();
            bool success = dbs_delete.DeleteCoach(coachId);

            if (success)
            {
                return Ok(new { message = "Coach deleted successfully" });
            }
            else
            {
                return NotFound(new { message = "Coach not found or error during deletion" });
            }
        }
    }
}
