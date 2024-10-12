using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerTypesController : ControllerBase
{
    private readonly ICustomerTypeRepository _customerTypeRepository;

    public CustomerTypesController(ICustomerTypeRepository customerTypeRepository)
    {
        _customerTypeRepository = customerTypeRepository;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<CustomerTypeModel?>> Post(
        CustomerTypeCreateDto customerTypeCreateDto
    )
    {
        var result = await _customerTypeRepository.CreateCustomerTypeAsync(customerTypeCreateDto);

        return Ok(result);
    }

    // Read

    [HttpGet]
    public async Task<ActionResult<List<CustomerTypeModel?>>> GetAllCustomerTypes()
    {
        var result = await _customerTypeRepository.GetAllCustomerTypesAsync();

        return Ok(result);
    }

    [HttpGet("{customerTypeId}")]
    public async Task<ActionResult<CustomerTypeModel?>> GetCustomerTypeById(int customerTypeId)
    {
        var result = await _customerTypeRepository.GetCustomerTypeByIdAsync(customerTypeId);
        return Ok(result);
    }

    // Update
    [HttpPut("{customerTypeId}")]
    public async Task<ActionResult> UpdateCustomerType(
        int customerTypeId,
        [FromBody] CustomerTypeCreateDto customerTypeCreateDto
    )
    {
        await _customerTypeRepository.UpdateCustomerTypeAsync(
            customerTypeId,
            customerTypeCreateDto
        );
        return Ok();
    }

    // Delete

    [HttpDelete("{customerTypeId}")]
    public async Task<ActionResult> DeleteCustomerType(int customerTypeId)
    {
        await _customerTypeRepository.DeleteCustomerTypeAsync(customerTypeId);
        return Ok();
    }
}
