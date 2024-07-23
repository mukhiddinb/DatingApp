using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController(DataContext context): BaseApiController
{
    [Authorize]
    [HttpGet( "auth" )]
    public ActionResult<string> GetAuth(){
        return "secret text";
    }

    [HttpGet( "not-found" )]
    public ActionResult<AppUser> GetNotFound(){
        var user = context.Users.Find(-1);

        if(user is null) return NotFound();

        return user;
    }

    [HttpGet( "server-error" )]
    public ActionResult<string> GetServerError(){
        var user = context.Users.Find(-1) ?? throw new Exception("A bad thing has happened");

        return "secret text";
    }

    [HttpGet( "bad-request" )]
    public ActionResult<string> GetBadRequest(){
        return BadRequest("This was not a good request");
    }
}
