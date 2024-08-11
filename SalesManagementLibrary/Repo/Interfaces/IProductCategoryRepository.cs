using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IProductCategoryRepository
{
    // Create
    Task<ProductCategoryModel?> CreateProductCategoryAsyc(ProductCategoryCreateDto productCategoryCreateDto);

    // Read
    Task<List<ProductCategoryModel?>> GetAllProductCategories();  
    Task<ProductCategoryModel?> GetProductCategoryById(int id);

    // Update
    Task UpdateProductCategory(int id, ProductCategoryCreateDto productCategoryCreateDto);

    // Delete
    Task DeleteProcuctCategory(int id);

}
