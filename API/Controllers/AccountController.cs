
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext dataContext, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
        if(await CheckUsername(registerDto.Username)){
            return BadRequest("Username is taken");
        }

        return Ok();

        // using var hmac = new HMACSHA512();

        // var user = new AppUser {
        //     UserName = registerDto.Username,
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     PasswordSalt = hmac.Key
        // };

        // dataContext.Users.Add(user);

        // await dataContext.SaveChangesAsync();

        // var result = new UserDto {
        //     Username = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };

        // return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginDto.Username.ToLower());

        if(user == null){ 
            return Unauthorized("User not found");
        }

        using var hmac = new HMACSHA512();

        hmac.Key = user.PasswordSalt;

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for(int i = 0; i<computedHash.Length; i++) {
            if(computedHash[i] != user.PasswordHash[i]){
                return Unauthorized ("Wrong password");
            }
        }

        var result = new UserDto {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };

        return Ok(result);
    }

    private async Task<bool> CheckUsername(string username){
        return await dataContext.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
    }
}
