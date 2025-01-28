using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IEntityRepository<RoleModel, RoleCreateDto> _roleRepository;

    public RolesController(IEntityRepository<RoleModel, RoleCreateDto> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<RoleModel?>> Post(RoleCreateDto roleCreateDto)
    {
        var result = await _roleRepository.CreateAsync(roleCreateDto);

        return Ok(result);
    }

    // Reade

    [HttpGet]
    public async Task<ActionResult<List<RoleModel>?>> GetAllRoles()
    {
        var result = await _roleRepository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{roleId}")]
    public async Task<ActionResult<RoleModel?>> GetById(int roleId)
    {
        var result = await _roleRepository.GetByIdAsync(roleId);
        return Ok(result);
    }

    // Update
    [HttpPut("{roleId}")]
    public async Task<ActionResult> UpdateRole(int roleId, [FromBody] RoleCreateDto roleCreateDto)
    {
        await _roleRepository.UpdateAsync(roleId, roleCreateDto);
        return Ok(new { message = "Updated Successfully" });
    }

    [HttpDelete("{roleId}")]
    public async Task<ActionResult> DeleteRole(int roleId)
    {
        await _roleRepository.DeleteAsync(roleId);
        return Ok(new { message = "Deleted" });
    }
}
