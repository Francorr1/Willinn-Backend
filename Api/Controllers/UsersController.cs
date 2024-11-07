using Microsoft.AspNetCore.Identity.Data;

namespace Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Models;
using Core.DTOs;
using Services;

[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    //Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
        if (user == null)
            return Unauthorized("Credenciales incorrectos");
        return Ok(user);
    }
    
    //Create User
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    { 
        await _userService.AddUserAsync(user, user.Password);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    //Get list of users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
    
    //Get a user
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    
    //Modify a user
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
            return BadRequest("Id de usuario no coincide");
        await _userService.UpdateUserAsync(user, user.Password);
        return NoContent();
    }
    
    //Delete a user
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}