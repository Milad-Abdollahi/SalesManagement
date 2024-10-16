using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public PaymentMethodRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<PaymentMethodModel?> CreatePaymentMethodAsync(
        PaymentMethodCreateDto paymentMethodCreateDto
    )
    {
        var parameters = new { MethodName = paymentMethodCreateDto.MethodName };
        var result = await _dapperDataAccess.LoadData<PaymentMethodModel?, dynamic>(
            "[dbo].[PaymentMethodInsert]",
            parameters,
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Read
    public async Task<List<PaymentMethodModel?>> GetAllPaymentMethodsAsync()
    {
        var result = await _dapperDataAccess.LoadData<PaymentMethodModel?>(
            "[dbo].[PaymentMethodsGetAll]",
            "DefaultConnection"
        );

        return result;
    }

    public async Task<PaymentMethodModel?> GetPaymentMethodByIdAsync(int id)
    {
        var result = await _dapperDataAccess.LoadData<PaymentMethodModel?, dynamic>(
            "[dbo].[PaymentMethodsGetById]",
            new { Id = id },
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Update
    public async Task UpdatePaymentMethodAsync(
        int id,
        PaymentMethodCreateDto paymentMethodCreateDto
    )
    {
        var parameters = new { Id = id, paymentMethodCreateDto.MethodName, };

        try
        {
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[PaymentMethodsUpdate]",
                parameters,
                "DefaultConnection"
            );
        }
        catch (SqlException ex) when (ex.Number == 2627) // Unique constraint error number
        {
            throw new InvalidOperationException(
                $"a payment method with this name already exists: {paymentMethodCreateDto.MethodName}",
                ex
            );
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    // Delete
    public Task DeletePaymentMethodAsync(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[PaymentMethodsDelete]",
            new { PaymentMethodId = id },
            "DefaultConnection"
        );
    }
}
