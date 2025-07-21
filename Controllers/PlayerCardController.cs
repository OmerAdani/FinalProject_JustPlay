using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCardController : Controller
    {
        /// Method to creat new player card
        [HttpPost("postCreatPlayerCard")]
        public ActionResult insertPlayerCardcon([FromBody] PlayerCard newPlayerCard)
        {
            int temp = newPlayerCard.addPlayerCard(newPlayerCard);
            if (temp == 1)
            {
                return Ok("Player Card added successfully");
            }
            return BadRequest("Error");
        }

        [HttpGet("byCoach/{coachId}")]
        public IActionResult GetPlayerCardsByCoachId(int coachId)
        {
            try
            {
                List<PlayerCard> cards = PlayerCard.ReadCardsByCoachId(coachId);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving player cards: {ex.Message}");
            }
        }

        [HttpPut("updatePlayerCard")]
        public IActionResult UpdatePlayerCard([FromBody] PlayerCard card)
        {
            PlayerCard updated = card.UpdatePlayerCard(card);

            if (updated == null)
            {
                return Conflict(new { message = "Card not found or already in use" });
            }

            return Ok(updated);
        }


        [HttpDelete("DeletePlayerCard")]
        public IActionResult DeletePlayerCard(int cardID)
        {
            PlayerCard card = new PlayerCard();
            bool result = card.DeletePlayerCard(cardID);

            if (result)
                return Ok(new { message = "Player card deleted successfully." });
            else
                return StatusCode(500, new { message = "Failed to delete player card." });
        }

        }
    }
