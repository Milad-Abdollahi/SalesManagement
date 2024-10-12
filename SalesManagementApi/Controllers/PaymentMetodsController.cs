using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMetodsController : ControllerBase
{
    private readonly IPaymentMetodRepository _paymentMethodRepository;

    public PaymentMetodsController(IPaymentMetodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    // Create
    // POST api/PaymentMethods
    [HttpPost]
    public async Task<ActionResult<PaymentMetodModel?>> CreatePaymentMethod(
        [FromBody] PaymentMetodCreateDto paymentMethodCreateDto
    )
    {
        var paymentMethod = await _paymentMethodRepository.CreatePaymentMetodAsync(
            paymentMethodCreateDto
        );
        if (paymentMethod == null)
        {
            return BadRequest();
        }
        return Ok(paymentMethod);
    }

    // Read
    // GET: api/MethodsController
    [HttpGet]
    public async Task<ActionResult<List<PaymentMetodModel>>> GetAllPaymentMethods()
    {
        var result = await _paymentMethodRepository.GetAllPaymentMetodsAsync();
        //return StatusCode(500, "internal Server error");
        return Ok(result);
    }

    // GET api/PaymentMethods/5
    [HttpGet("{paymentMethodId}")]
    public async Task<ActionResult<PaymentMetodModel>> Get(int paymentMethodId)
    {
        var result = await _paymentMethodRepository.GetPaymentMetodByIdAsync(paymentMethodId);
        return Ok(result);
    }

    // Update
    // PUT api/PaymentMethods/5
    [HttpPut("{paymentMethodId}")]
    public async Task<ActionResult> Put(
        int paymentMethodId,
        [FromBody] PaymentMetodCreateDto paymentMethodCreateDto
    )
    {
        {
            try
            {
                await _paymentMethodRepository.UpdatePaymentMetodAsync(
                    paymentMethodId,
                    paymentMethodCreateDto
                );
                return Ok(new { message = "Updated Successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { message = "An unexpected error occurred.", details = ex.Message }
                );
            }
        }
    }

    // DELETE api/PaymentMethods/5
    [HttpDelete("{paymentMethodId}")]
    public async Task<ActionResult> Delete(int paymentMethodId)
    {
        await _paymentMethodRepository.DeletePaymentMetodAsync(paymentMethodId);
        return Ok();
    }
}
