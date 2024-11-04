using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentStatusesController : ControllerBase
{
    private readonly IPaymentStatusRepository _paymentStatusRepository;

    public PaymentStatusesController(IPaymentStatusRepository paymentStatusRepository)
    {
        _paymentStatusRepository = paymentStatusRepository;
    }

    // Create
    // POST api/<PaymentStatusesController>
    [HttpPost]
    public async Task<ActionResult<PaymentStatusModel?>> Post(
        PaymentStatusCreateDto paymentStatusCreateDto
    )
    {
        var result = await _paymentStatusRepository.CreateAsync(paymentStatusCreateDto);

        return Ok(result);
    }

    // Read
    // GET: api/PaymentStatuses
    [HttpGet]
    public async Task<ActionResult<List<PaymentStatusModel>>> Get()
    {
        var result = await _paymentStatusRepository.GetAllAsync();
        return Ok(result);
    }

    // GET api/PaymentStatuses/5
    [HttpGet("{paymentStatusId}")]
    public async Task<ActionResult<PaymentStatusModel>> Get(int paymentStatusId)
    {
        var result = await _paymentStatusRepository.GetByIdAsync(paymentStatusId);
        return Ok(result);
    }

    // Update
    // PUT api/<PaymentStatusesController>/5
    [HttpPut("{paymentStatusId}")]
    public async Task<ActionResult> Put(
        int paymentStatusId,
        [FromBody] PaymentStatusCreateDto paymentStatusCreateDto
    )
    {
        await _paymentStatusRepository.UpdateAsync(paymentStatusId, paymentStatusCreateDto);
        return Ok(new { message = "Updated Successfully" });
    }

    // DELETE api/PaymentStatuses/5
    [HttpDelete("{paymentStatusId}")]
    public async Task<ActionResult> Delete(int paymentStatusId)
    {
        await _paymentStatusRepository.DeleteAsync(paymentStatusId);
        return Ok(new { message = "Deleted Successfully" });
    }
}
