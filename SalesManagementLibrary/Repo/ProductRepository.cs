using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

// Branch2 Added

public class ProductRepository : IProductRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public ProductRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<ProductModel?> CreateProductAsync(ProductCreateDto productCreateDto)
    {
        var parameter = new
        {
            Name = productCreateDto.Name,
            Description = productCreateDto.Description,
            Price = productCreateDto.Price,
            StockQuantity = productCreateDto.StockQuantity,
            ProductCategoryId = productCreateDto.Category.Id,
            CreatedDate = productCreateDto.CreatedDate,
        };

        var result = await _dapperDataAccess.LoadData<ProductModel?>(
            "[dbo].[ProductInsert]",
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductModel?>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel?> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(int id, ProductCreateDto productCreateDto)
    {
        throw new NotImplementedException();
    }
}
