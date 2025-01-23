using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IEntityRepository<
        CustomerModel,
        CustomerCreateDto
    > _customerRepository;

    public CustomersController(
        IEntityRepository<CustomerModel, CustomerCreateDto> customerRepository
    )
    {
        _customerRepository = customerRepository;
    }

    // Create

    [HttpPost]
    public async Task<ActionResult<CustomerModel?>> Post(
        CustomerCreateDto customerCreateDto
    )
    {
        var result = await _customerRepository.CreateAsync(customerCreateDto);

        return Ok(result);
    }

    // Read


    [HttpGet]
    public async Task<ActionResult<List<CustomerModel>>> GetAllCustomers()
    {
        var result = await _customerRepository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{customerId}")]
    public async Task<ActionResult<CustomerModel>> GetCustomerById(
        int customerId
    )
    {
        var result = await _customerRepository.GetByIdAsync(customerId);
        return Ok(result);
    }

    // Update

    [HttpPut("{customerId}")]
    public async Task<ActionResult> UpdateCustomer(
        int customerId,
        [FromBody] CustomerCreateDto customerCreateDto
    )
    {
        await _customerRepository.UpdateAsync(customerId, customerCreateDto);
        return Ok(new { message = "Updated Successfully" });
    }

    // Delete
    [HttpDelete("{customerId}")]
    public async Task<ActionResult> DeleteCustomer(int customerId)
    {
        await _customerRepository.DeleteAsync(customerId);
        return Ok();
    }
}
