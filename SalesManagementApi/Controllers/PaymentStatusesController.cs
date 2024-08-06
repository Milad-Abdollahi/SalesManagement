using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;
using System.Diagnostics.CodeAnalysis;

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
    public async Task<ActionResult<PaymentStatusModel?>> Post(PaymentStatusCreateDto paymentStatusCreateDto)
    {
        var result = await _paymentStatusRepository.CreatePaymentStatusAsync(paymentStatusCreateDto);

        if (result == null)
        {
            return BadRequest("Payment Method Creation Failed!");
        }

        return Ok(result);
    }

    // Read
    // GET: api/PaymentStatuses
    [HttpGet]
    public async Task<ActionResult<List<PaymentStatusModel>>> Get()
    {
        var result = await _paymentStatusRepository.GetAllPaymentStatusesAsync();
        return Ok(result);
    }

    // GET api/PaymentStatuses/5
    [HttpGet("{paymentStatusId}")]
    public async Task<ActionResult<PaymentStatusModel>> Get(int paymentStatusId)
    {
        var result = await _paymentStatusRepository.GetPaymentStatusByIdAsync(paymentStatusId);
        return Ok(result);
    }

    
    // Update
    // PUT api/<PaymentStatusesController>/5
    [HttpPut("{paymentStatusId}")]
    public async Task<ActionResult> Put(int paymentStatusId, [FromBody] PaymentStatusCreateDto paymentStatusCreateDto)
    {
        await _paymentStatusRepository.UpdatePaymentStatusAsync(paymentStatusId, paymentStatusCreateDto);
        return Ok();
    }

    // DELETE api/PaymentStatuses/5
    [HttpDelete("{paymentStatusId}")]
    public async Task<ActionResult> Delete(int paymentStatusId)
    {
        await _paymentStatusRepository.DeletePaymentStatusAsync(paymentStatusId);
        return Ok();
    }
}
