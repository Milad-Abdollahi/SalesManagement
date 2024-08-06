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



    // GET: api/<PaymentsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<PaymentsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }


    // PUT api/<PaymentsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PaymentsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
