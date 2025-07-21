using Final_project1.BL;
using Final_project1.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipatesInTrainingController : Controller
    {
        [HttpGet("byCoach/{coachId}")]
        public IActionResult GetTrainingParticipationsByCoachId(int coachId)
        {
            try
            {
                List<ParticipatesInTraining> participations = ParticipatesInTraining.ReadByCoachId(coachId);
                return Ok(participations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving training participations: {ex.Message}");
            }
        }
    }
}
