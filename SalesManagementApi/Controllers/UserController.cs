using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Create

    // POST api/<UserController>
    [HttpPost]
    public async Task<ActionResult<UserModel?>> CreateUser(UserCreateDto userCreateDto)
    {
        var user = await _userRepository.CreateAsync(userCreateDto);
        if (user == null)
        {
            return BadRequest("User Creation Failed");
        }

        return Ok(user);
    }



    // Read
    [HttpGet]
    // GET: api/<UserController>
    public async Task<ActionResult<List<UserModel>>> Get()
    {
        var output = await _userRepository.GetAllUsers();
        return Ok(output);
    }



    // GET api/User/5
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserModel>> Get(int userId)
    {
        var output = await _userRepository.GetUserById(userId);
        return Ok(output);
    }




    // Update 

    // PUT api/User/5
    [HttpPut("{userId}")]
    public async Task<ActionResult> Put(int userId, [FromBody] UserCreateDto userCreateDto)
    {
        await _userRepository.UpdateUser(userId, userCreateDto);
        return Ok();
    }




    // DELETE api/<UserController>/5
    [HttpDelete("{userId}")]
    public async void Delete(int userId)
    {
        await _userRepository.DeleteUser(userId);
    }
}
