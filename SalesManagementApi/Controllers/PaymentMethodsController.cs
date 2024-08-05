using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodsController : ControllerBase
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public PaymentMethodsController(IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }


    // Create
    // POST api/PaymentMethods
    [HttpPost]
    public async Task<ActionResult<PaymentMethodModel?>> CreatePaymentMethod([FromBody] PaymentMethodCreateDto paymentMethodCreateDto)
    {
        var paymentMethod = await _paymentMethodRepository.CreatPaymentMethodAsync(paymentMethodCreateDto);
        if (paymentMethod == null)
        {
            return BadRequest();
        }
        return Ok(paymentMethod);
    }





    // GET: api/<PaymentMethodsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<PaymentMethodsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }


    // PUT api/<PaymentMethodsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PaymentMethodsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
