using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class CustomerTypeRepository : ICustomerTypeRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public CustomerTypeRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<CustomerTypeModel?> CreateCustomerTypeAsync(
        CustomerTypeCreateDto customerTypeCreateDto
    )
    {
        var result = await _dapperDataAccess.LoadData<CustomerTypeModel?, dynamic>(
            "[dbo].[CustomerTypeInsert]",
            new { TypeName = customerTypeCreateDto.TypeName },
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Read
    public async Task<List<CustomerTypeModel?>> GetAllCustomerTypesAsync()
    {
        var result = await _dapperDataAccess.LoadData<CustomerTypeModel?>(
            "[dbo].[CustomerTypesGetAll]",
            "DefaultConnection"
        );

        return result;
    }

    public async Task<CustomerTypeModel?> GetCustomerTypeByIdAsync(int id)
    {
        var result = await _dapperDataAccess.LoadData<CustomerTypeModel?, dynamic>(
            "[dbo].[CustomerTypesGetById]",
            new { CustomerTypeId = id },
            "DefaultConnection"
        );
        return result.FirstOrDefault();
    }

    // Update
    public Task UpdateCustomerTypeAsync(int id, CustomerTypeCreateDto customerTypeCreateDto)
    {
        var parameter = new { CustomerTypeId = id, TypeName = customerTypeCreateDto.TypeName };
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[CustomerTypesUpdate]",
            parameter,
            "DefaultConnection"
        );
    }

    // Delete
    public Task DeleteCustomerTypeAsync(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[CustomerTypesDelete]",
            new { CustomerTypeId = id },
            "DefaultConnection"
        );
    }
}
