using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController : ControllerBase
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRolesController(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    // Create
    // POST api/<UserRolesController>
    [HttpPost]
    public async Task<ActionResult<UserRoleModel?>> CreateUserRole(UserRoleCreateDto userRoleCreateDto)
    {
        var userRole = await _userRoleRepository.CreateAsync(userRoleCreateDto);

        if (userRole == null)
        {
            return BadRequest("UserRole Creation Failed");
        }

        return Ok(userRole);
    }

    // Read
    // GET: api/UserRoles
    [HttpGet]
    public async Task<ActionResult<List<UserRoleModel>>> GetAllUserRoles()
    {
        var result = await _userRoleRepository.GetAllUserRoleModelsAsync();
        return Ok(result);
    }

    // GET api/UserRoles/5
    [HttpGet("{userRoleId}")]
    public async Task<ActionResult<UserRoleModel>> Get(int userRoleId)
    {
        var result = await _userRoleRepository.GetUserRoleByIdAsync(userRoleId);
        return Ok(result);
    }

    

    // Update
    // PUT api/<UserRolesController>/5
    [HttpPut("{userRoleId}")]
    public async Task<ActionResult> Put(int userRoleId, [FromBody] UserRoleCreateDto userRoleCreateDto)
    {
        await _userRoleRepository.UpdateUserRoleAsync(userRoleId, userRoleCreateDto);
        return Ok();
    }



    // DELETE api/<UserRolesController>/5
    [HttpDelete("{userRoleId}")]
    public async Task<ActionResult> Delete(int userRoleId)
    {
        await _userRoleRepository.DeleteUserRoleAsync(userRoleId);
        return Ok();
    }
}
