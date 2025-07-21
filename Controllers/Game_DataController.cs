using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Game_DataController : Controller
    {

        [HttpGet("Get_Game_Data_chatGPT/{CoachID}")]
        public IActionResult GetGameDataByCoachId_GPT([FromRoute] int CoachID)
        {
            try
            {
                // 1. קבלת הנתונים
                List<Game_Data> gd = Game_Data.ReadDataByCoachId(CoachID);

                // 2. אם אין נתונים – החזרה פשוטה
                if (gd == null || gd.Count == 0)
                {
                    var emptyResult = new
                    {
                        data = gd,
                        chatgpt_response = "לא נמצאו נתוני משחקים עבור הקבוצה שלך. נא להוסיף נתונים כדי לקבל תובנות מקצועיות מהמערכת."
                    };

                    return Ok(emptyResult);
                }

                // 3. יש נתונים – נשלח ל־ChatGPT
                string jsonStats = JsonSerializer.Serialize(gd, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                string prompt = @"אתה מאמן בקבוצת כדורגל מקצוענית.

קיבלת טבלת נתונים שמכילה מידע על מספר שחקנים ממספר משחקים שונים. 
כל שורה בטבלה מייצגת ביצועים של שחקן מסוים במשחק אחד.

המשימה שלך:
1. לקבץ את הנתונים לפי שחקן.
2. לנתח כל שחקן לפי התפקיד שלו במגרש (עמדתו).
3. לזהות דפוסים לאורך מספר משחקים – חוזקות וחולשות.
4. להציע הערות מקצועיות לשיפור לכל שחקן.
אני מבקש לחדד את הנושא, לתת לכל שחקן המלצה והערות לפי תפקידו ,לדוגמה אם שחקן הוא בלם והוא לא בעט לשער זה דבר לגיטימי ואין צורך לתת לו הערה לשיפור על זה .
כל הערה צריכה להתאים לתפקיד של השחקן.

החזר את התוצאה כ־JSON בלבד. 
אל תשתמש בתחביר Markdown, אל תשתמש ב-```json או ```.
רק JSON נקי.:
[
  {
    ""player_id"": 123,
    ""player_name"": ""דוד לוי"",
    ""position"": ""קשר התקפי"",
    ""comment"": ""השחקן מצליח לשמור על יציבות במסירות אך תורם מעט מבחינה התקפית."",
    ""suggestion"": ""להתמקד בשיפור קבלת החלטות בשליש האחרון של המגרש.""
  }
]
אם אתה מקבל רשימה ריקה או בלי ערכים תחזיר תשובה:
[
  {""comment"":""כרגע לא קיימים נתוני משחקים עבור הקבוצה שלך. נא להוסיף נתונים כדי לקבל תובנות מקצועיות מהמערכת""}
]

הנה הנתונים:
" + jsonStats;

                string chatgpt_response = ChatGPTClient.GetChatGPTResponseAsync(prompt);

                var result = new
                {
                    data = gd,
                    chatgpt_response = chatgpt_response
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }
    





        [HttpPost("insertGameData")]
        public IActionResult InsertParticipation([FromBody] Game_Data newGameData)
        {
            try
            {
                int result = newGameData.InsertGameData(newGameData);
                if (result > 0)
                    return Ok(new { message = "Data inserted successfully." });
                else
                    return BadRequest("No record was inserted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpGet("by-coach/{id}")]
        public IActionResult GetGameDataByCoachId([FromRoute(Name = "id")] int coachId)
        {
            try
            {
                List<Game_Data> data = Game_Data.ReadDataByCoachId(coachId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving game data: {ex.Message}");
            }
        }



        [HttpPut("updatePlayerGameStats")]
        public IActionResult UpdatePlayerGameStats([FromBody] Game_Data gameData)
        {
            Game_Data bll = new Game_Data();
            var updatedStats = bll.UpdateStats(gameData);

            if (updatedStats == null)
            {
                return BadRequest(new { message = "עדכון נכשל - ייתכן שהשחקן או המשחק לא נמצאו." });
            }

            return Ok(new
            {
                message = "Player stats updated successfully",
                data = updatedStats
            });
        }




        [HttpDelete("deletePlayerFromGame")]
        public IActionResult DeletePlayerFromGame([FromQuery] int playerId, [FromQuery] int gameId)
        {
            Game_Data gd = new Game_Data();
            bool success = gd.RemovePlayerFromGame(playerId, gameId);

            if (success)
            {
                return Ok(new { message = "Player game data deleted successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to delete player game data" });
            }
        }

    }
}





