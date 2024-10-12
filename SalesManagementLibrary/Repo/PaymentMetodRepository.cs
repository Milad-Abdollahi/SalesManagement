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

public class PaymentMetodRepository : IPaymentMetodRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public PaymentMetodRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<PaymentMetodModel?> CreatePaymentMetodAsync(
        PaymentMetodCreateDto paymentMetodCreateDto
    )
    {
        var parameters = new { MetodName = paymentMetodCreateDto.MetodName };
        var result = await _dapperDataAccess.LoadData<PaymentMetodModel?, dynamic>(
            "[dbo].[PaymentMetodInsert]",
            parameters,
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Read
    public async Task<List<PaymentMetodModel?>> GetAllPaymentMetodsAsync()
    {
        var result = await _dapperDataAccess.LoadData<PaymentMetodModel?>(
            "[dbo].[PaymentMetodsGetAll]",
            "DefaultConnection"
        );

        return result;
    }

    public async Task<PaymentMetodModel?> GetPaymentMetodByIdAsync(int id)
    {
        var result = await _dapperDataAccess.LoadData<PaymentMetodModel?, dynamic>(
            "[dbo].[PaymentMetodsGetById]",
            new { Id = id },
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Update
    public async Task UpdatePaymentMetodAsync(int id, PaymentMetodCreateDto paymentMetodCreateDto)
    {
        var parameters = new { Id = id, paymentMetodCreateDto.MetodName, };

        try
        {
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[PaymentMetodsUpdate]",
                parameters,
                "DefaultConnection"
            );
        }
        catch (SqlException ex) when (ex.Number == 2627) // Unique constraint error number
        {
            throw new InvalidOperationException(
                $"a payment method with this name already exists: {paymentMetodCreateDto.MetodName}",
                ex
            );
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    // Delete
    public Task DeletePaymentMetodAsync(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[PaymentMetodsDelete]",
            new { PaymentMetodId = id },
            "DefaultConnection"
        );
    }
}
