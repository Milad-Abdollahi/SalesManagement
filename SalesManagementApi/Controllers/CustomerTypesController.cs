using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerTypesController : ControllerBase
{
    private readonly IEntityRepository<CustomerTypeModel, CustomerTypeCreateDto> _customerTypeRepository;

    public CustomerTypesController(IEntityRepository<CustomerTypeModel, CustomerTypeCreateDto> customerTypeRepository)
    {
        _customerTypeRepository = customerTypeRepository;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<CustomerTypeModel?>> Post(CustomerTypeCreateDto customerTypeCreateDto)
    {
        var result = await _customerTypeRepository.CreateAsync(customerTypeCreateDto);

        return Ok(result);
    }

    // Read

    [HttpGet]
    public async Task<ActionResult<List<CustomerTypeModel?>>> GetAllCustomerTypes()
    {
        var result = await _customerTypeRepository.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{customerTypeId}")]
    public async Task<ActionResult<CustomerTypeModel?>> GetCustomerTypeById(int customerTypeId)
    {
        var result = await _customerTypeRepository.GetByIdAsync(customerTypeId);
        return Ok(result);
    }

    // Update

    [HttpPut("{customerTypeId}")]
    public async Task<ActionResult> UpdateCustomerType(
        int customerTypeId,
        [FromBody] CustomerTypeCreateDto customerTypeCreateDto
    )
    {
        await _customerTypeRepository.UpdateAsync(customerTypeId, customerTypeCreateDto);
        return Ok(new { message = "Updated Successfully" });
    }

    // Delete

    [HttpDelete("{customerTypeId}")]
    public async Task<ActionResult> DeleteCustomerType(int customerTypeId)
    {
        await _customerTypeRepository.DeleteAsync(customerTypeId);
        return Ok();
    }
}
