using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoriesController(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    // Create

    // POST api/<ProductCategoriesController>
    [HttpPost]
    public async Task<ActionResult<ProductCategoryModel?>> Post(
        [FromBody] ProductCategoryCreateDto productCategoryCreateDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productCategoryRepository.CreateProductCategoryAsyc(
            productCategoryCreateDto
        );

        return Ok(result);
    }

    // Read
    // GET: api/ProductCategories
    [HttpGet]
    public async Task<ActionResult<List<ProductCategoryModel?>>> Get()
    {
        var result = await _productCategoryRepository.GetAllProductCategories();
        return Ok(result);
    }

    // GET api/<ProductCategoriesController>/5
    [HttpGet("{productCategoryId}")]
    public async Task<ActionResult<ProductCategoryModel?>> Get(int productCategoryId)
    {
        var result = await _productCategoryRepository.GetProductCategoryById(productCategoryId);
        return Ok(result);
    }

    // Update
    // PUT api/<ProductCategoriesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(
        int id,
        [FromBody] ProductCategoryCreateDto productCategoryCreateDto
    )
    {
        await _productCategoryRepository.UpdateProductCategory(id, productCategoryCreateDto);
        return Ok();
    }

    // DELETE api/<ProductCategoriesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _productCategoryRepository.DeleteProcuctCategory(id);
        return Ok();
    }
}
