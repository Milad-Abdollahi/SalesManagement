using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public ProductCategoryRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<ProductCategoryModel?> CreateProductCategoryAsyc(
        ProductCategoryCreateDto productCategoryCreateDto
    )
    {
        var parameter = new { CategoryName = productCategoryCreateDto.CategoryName, };

        var result = await _dapperDataAccess.LoadData<ProductCategoryModel?, dynamic>(
            "[dbo].[ProductCategoryInsert]",
            parameter,
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Read
    public async Task<List<ProductCategoryModel?>> GetAllProductCategories()
    {
        var result = await _dapperDataAccess.LoadData<ProductCategoryModel?>(
            "[dbo].[ProductCategoriesGetAll]",
            "DefaultConnection"
        );
        return result;
    }

    public async Task<ProductCategoryModel?> GetProductCategoryById(int id)
    {
        var result = await _dapperDataAccess.LoadData<ProductCategoryModel?, dynamic>(
            "[dbo].[ProductCategoriesGetById]",
            new { ProductCategoryId = id },
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Update
    public Task UpdateProductCategory(int id, ProductCategoryCreateDto productCategoryCreateDto)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[ProductCategoriesUpdate]",
            new { ProductCategoryId = id, CategoryName = productCategoryCreateDto.CategoryName },
            "DefaultConnection"
        );
    }

    // Delete
    public Task DeleteProcuctCategory(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[ProductCategoriesDelete]",
            new { ProductCategoryId = id },
            "DefaultConnection"
        );
    }
}
