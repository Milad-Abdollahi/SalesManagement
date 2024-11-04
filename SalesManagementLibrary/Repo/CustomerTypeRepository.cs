using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.BaseRepo;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class CustomerTypeRepository : BaseRepository, ICustomerTypeRepository
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
        return await ExecWithErrHandling<CustomerTypeModel?>(async () =>
        {
            var parameter = new { TypeName = customerTypeCreateDto.TypeName };
            List<CustomerTypeModel?> result = await _dapperDataAccess.LoadData<
                CustomerTypeModel?,
                dynamic
            >("[dbo].[CustomerTypeInsert]", parameter, "DefaultConnection");

            return result.FirstOrDefault();
        });
    }

    // Read
    public async Task<List<CustomerTypeModel?>> GetAllCustomerTypesAsync()
    {
        return await ExecWithErrHandling(async () =>
        {
            var result = await _dapperDataAccess.LoadData<CustomerTypeModel?>(
                "[dbo].[CustomerTypesGetAll]",
                "DefaultConnection"
            );

            return result;
        });
    }

    public async Task<CustomerTypeModel?> GetCustomerTypeByIdAsync(int id)
    {
        return await ExecWithErrHandling<CustomerTypeModel?>(async () =>
        {
            var result = await _dapperDataAccess.LoadData<CustomerTypeModel?, dynamic>(
                "[dbo].[CustomerTypesGetById]",
                new { CustomerTypeId = id },
                "DefaultConnection"
            );
            return result.FirstOrDefault();
        });
    }

    // Update
    public async Task UpdateCustomerTypeAsync(int id, CustomerTypeCreateDto customerTypeCreateDto)
    {
        await ExecWithErrHandling(async () =>
        {
            var parameter = new { CustomerTypeId = id, TypeName = customerTypeCreateDto.TypeName };
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[CustomerTypesUpdate]",
                parameter,
                "DefaultConnection"
            );
        });
    }

    // Delete
    public async Task DeleteCustomerTypeAsync(int id)
    {
        await ExecWithErrHandling(async () =>
        {
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[CustomerTypesDelete]",
                new { CustomerTypeId = id },
                "DefaultConnection"
            );
        });
    }
}
