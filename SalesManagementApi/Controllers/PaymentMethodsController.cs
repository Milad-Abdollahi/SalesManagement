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
        var paymentMethod = await _paymentMethodRepository.CreatePaymentMethodAsync(paymentMethodCreateDto);
        if (paymentMethod == null)
        {
            return BadRequest();
        }
        return Ok(paymentMethod);
    }



    // Read

    // GET: api/MethodsController
    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodModel>>> GetAllPaymentMethods()
    {
        var result = await _paymentMethodRepository.GetAllPaymentMethodsAsync();
        return Ok(result);  
    }

    // GET api/PaymentMethods/5
    [HttpGet("{paymentMethodId}")]
    public async Task<ActionResult<PaymentMethodModel>> Get(int paymentMethodId)
    {
        var result = await _paymentMethodRepository.GetPaymentMethodByIdAsync(paymentMethodId);
        return Ok(result);
    }


    // Update
    // PUT api/PaymentMethods/5
    [HttpPut("{paymentMethodId}")]
    public async Task<ActionResult> Put(int paymentMethodId, [FromBody] PaymentMethodCreateDto paymentMethodCreateDto)
    {
        await _paymentMethodRepository.UpdatePaymentMethodAsync(paymentMethodId, paymentMethodCreateDto);
        return Ok();
    }

    // DELETE api/PaymentMethods/5
    [HttpDelete("{paymentMethodId}")]
    public async Task<ActionResult> Delete(int paymentMethodId)
    {
        await _paymentMethodRepository.DeletePaymentMethodAsync(paymentMethodId);
        return Ok();
    }
}
