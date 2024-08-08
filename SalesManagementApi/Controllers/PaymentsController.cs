using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentsController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }


    // Create
    // POST api/Payments
    [HttpPost]
    public async Task<ActionResult<PaymentModel?>> Post([FromBody] PaymentCreateDto paymentCreateDto)
    {
        var payment = await _paymentRepository.CreatePaymentAsync(paymentCreateDto);

        if (payment == null)
        {
            return BadRequest("Payment creation Failed!");
        }

        return Ok(payment);
    }


    // Read
    // GET: api/<PaymentsController>
    [HttpGet]
    public async Task<ActionResult<List<PaymentModel>>> Get()
    {
        var result = await _paymentRepository.GetAllPaymentsAsync();
        return Ok(result);
    }

    // GET api/<PaymentsController>/5
    [HttpGet("{paymentId}")]
    public async Task<ActionResult<PaymentModel>> Get(int paymentId)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
        return Ok(payment);
    }

    // Update
    // PUT api/Payments/5
    [HttpPut("{paymentId}")]
    public async Task<ActionResult> Put(int paymentId, [FromBody] PaymentCreateDto paymentCreateDto)
    {
        await _paymentRepository.UpdatePaymentAsyc(paymentId, paymentCreateDto);
        return Ok();
    }


    // Delete
    // DELETE api/<PaymentsController>/5
    [HttpDelete("{paymentId}")]
    public async Task<ActionResult> Delete(int paymentId)
    {
        await _paymentRepository.DeletePaymentAsync(paymentId);
        return Ok();
    }
}
