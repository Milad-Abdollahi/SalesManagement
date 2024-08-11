using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IProductRepository
{
    // Create
    Task<ProductModel?> CreateProductAsync(ProductCreateDto productCreateDto);

    // Read
    Task<List<ProductModel?>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(int id);

    // Update
    Task UpdateProductAsync(int id, ProductCreateDto productCreateDto);

    // Delete
    Task DeleteProductAsync(int id);
}
