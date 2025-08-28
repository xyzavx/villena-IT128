using BlogDataLibrary.Database;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly ISqlDataAccess _db;

    public PostsController(ISqlDataAccess db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<ListPostModel>> GetAllPosts()
    {
        string sql = "SELECT p.Id, p.Title, p.Body, p.DateCreated, u.UserName, u.FirstName, u.LastName " +
                     "FROM Posts p INNER JOIN Users u ON p.UserId = u.Id";

        return _db.LoadData<ListPostModel, dynamic>(sql, new { }, "SqlDb", false);
    }

    [HttpPost]
    public void AddPost([FromBody] PostModel post)
    {
        string sql = "INSERT INTO Posts (UserId, Title, Body, DateCreated) VALUES (@UserId, @Title, @Body, @DateCreated)";
        _db.SaveData(sql, post, "SqlDb", false);
    }
}
