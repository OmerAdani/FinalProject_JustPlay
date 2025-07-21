using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {


        [HttpGet("GetPlayerbyCoachID/{coachId}")]
        public IActionResult GetPlayersByCoachId(int coachId)
        {
            try
            {
                List<Player> players = Player.ReadPlayersByCoachId(coachId);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving players: {ex.Message}");
            }
        }




        /// Method to creat new player
        [HttpPost("postCreatPlayer")]
        public ActionResult insertPlayercon([FromBody] Player newPlayer)
        {
            //Player tempPlayer = new Player();
            int temp = newPlayer.Register(newPlayer);
            if (temp == 1)
            {
                return Ok(new { message = "Player added successfully" });
            }
            return BadRequest("Error or existing player");
        }

       

        


        [HttpPut("updateplayer")]
        public IActionResult updatePlayercon([FromBody] Player player)
        {
            Player updatedplayer = new Player();
            Player temp = updatedplayer.UpdatePlayer(player);

            if (temp == null)
            {
                return Conflict(new { message = "User not found or email is already in use" });
            }

            return Ok(player);
        }


        [HttpDelete("deletePlayer/{playerId}")]
        public IActionResult DeletePlayer(int playerId)
        {
            bool success = new Player().DeletePlayer(playerId);

            if (success)
            {
                return Ok(new { message = "Player deleted successfully" });
            }
            else
            {
                return NotFound(new { message = "Player not found or error during deletion" });
            }
        }
    }
}
