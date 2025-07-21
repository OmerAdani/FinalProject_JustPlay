using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : Controller
    {
        /// Method to creat new training
        [HttpPost("postCreatTraining")]
        public ActionResult insertTrainingcon([FromBody] Training newTraining)
        {
            int temp = newTraining.addTraining(newTraining);
            if (temp == 1)
            {
                return Ok("Training added successfully");
            }
            return BadRequest("Error");
        }


        [HttpGet("Get Training list")]
        public List<Training> getTrainingListCon()
        {
            return Training.ReadTraining();
        }



        [HttpPut("updateTraining")]
        public IActionResult UpdateTraining([FromBody] Training training)
        {
            Training updated = new Training().UpdateTraining(training);

            if (updated == null)
            {
                return NotFound(new { message = "Training not found or update failed" });
            }

            return Ok(updated);
        }


        [HttpDelete("Delete")]
        public IActionResult DeleteTraining(int trainingID)
        {
            Training training = new Training();
            bool result = training.DeleteTarining(trainingID);

            if (result)
                return Ok(new { message = "Training deleted successfully." });
            else
                return StatusCode(500, new { message = "Failed to delete training." });
        }

        
        }
}
