using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        [HttpGet("byCoach/{coachId}")]
        public IActionResult GetGamesByCoachId(int coachId)
        {
            try
            {
                List<Game> games = Game.ReadGamesByCoachId(coachId);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving games: {ex.Message}");
            }
        }



        [HttpPost("postCreatGame")]
        public ActionResult insertGamehcon([FromBody] Game newGame)
        {
            int temp = newGame.addNewGame(newGame);
            if (temp == 1)
            {
                return Ok(new { message = "Game added successfully" });
            }
            return BadRequest("Error");
        }


        [HttpPut("updategame")]
        public IActionResult UpdateGame([FromBody] Game game)
        {
            Game updatedGame = game.UpdateGame(game);

            if (updatedGame == null)
            {
                return Conflict(new { message = "Game not found, duplicate date for team, or update failed." });
            }

            return Ok(new { message = "Game updated successfully" });
        }



        [HttpDelete("DeleteGame")]
        public IActionResult DeleteGame(int GameID)
        {
            Game game = new Game();
            bool result = game.DeleteGame(GameID);

            if (result)
                return Ok(new { message = "Game deleted successfully." });
            else
                return StatusCode(500, new { message = "Failed to delete game." });
        }
    }
}
