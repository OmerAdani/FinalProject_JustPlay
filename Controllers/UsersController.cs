using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Final_project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

      

        [HttpGet("Get user list")]
       
        public IEnumerable<Users> GetRead()
        {
            return Users.ReadUsers();
        }






        [HttpPost("PostLogin")]
        public IActionResult Login(string Email, string Password)
        {
            Users user = new Users(); // יצירת מופע של המחלקה Users

            // ניסיון להתחבר עם פרטי הכניסה שסופקו
            bool Login = user.Login(Email, Password);
            bool isLogin = Login;
            if (isLogin)
            {
                // שליפת פרטי המשתמש לפי כתובת האימייל
                var (id,team_name,name, isActive,role,isManager) = user.GetUserIdByEmail(Email);

           

                // בדיקה אם המשתמש פעיל
                if (!isActive)
                {
                    return NotFound(new { message = "false" }); // המשתמש קיים אך לא פעיל
                }

                if (id > 0)
                {
                    return Ok(new
                    {
                        id = id,     // החזרת מזהה המשתמש
                        name = name,  //
                        team_name = team_name,
                        role = role,
                        isManager= isManager
                    });
                }

                return NotFound(new { message = "User ID not found" }); // לא נמצא מזהה משתמש תקין
            }

            return Unauthorized(new { message = "Invalid email or password" }); // התחברות נכשלה
        }





    }
}
