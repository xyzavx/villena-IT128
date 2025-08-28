using BlogDataLibrary.Database;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISqlDataAccess _db;

    public AuthController(ISqlDataAccess db)
    {
        _db = db;
    }

    [HttpPost("register")]
    public void Register([FromBody] UserModel user)
    {
        string sql = "INSERT INTO Users (UserName, FirstName, LastName, Password) VALUES (@UserName, @FirstName, @LastName, @Password)";
        _db.SaveData(sql, user, "SqlDb", false);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserModel login)
    {
        string sql = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
        var users = _db.LoadData<UserModel, UserModel>(sql, login, "SqlDb", false);
        if (users.Count > 0)
            return Ok(users[0]);
        else
            return Unauthorized();
    }
}
