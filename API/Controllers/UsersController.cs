using System.Security.Claims;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(){
        //var users = await userRepository.GetAllAsync();

        //var result = mapper.Map<IEnumerable<MemberDto>>(users);

        var result = await userRepository.GetAllMembersAsync();

        return Ok(result);
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<AppUser>> GetUser(int id){
    //     var user = await userRepository.GetUserByIdAsync(id);

    //     if(user is null) return NotFound();

    //     return user;
    // }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username){
        //var user = await userRepository.GetUserByUsernameAsync(username);
        var user = await userRepository.GetMemberByUsernameAsync(username);

        if(user is null) return NotFound();

        //var result = mapper.Map<MemberDto>(user);

        return user;//result;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto){
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(string.IsNullOrWhiteSpace(username)) return BadRequest("Could not find the username in the token");

        var user = await  userRepository.GetUserByUsernameAsync(username);

        if (user is null) return BadRequest("Could not find the user");

        mapper.Map(memberUpdateDto, user);

        if(await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }
}
