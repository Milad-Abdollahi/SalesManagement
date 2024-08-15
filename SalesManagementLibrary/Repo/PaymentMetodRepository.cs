using System;
using System.Collections.Generic;
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
    public Task UpdatePaymentMetodAsync(int id, PaymentMetodCreateDto paymentMetodCreateDto)
    {
        var parameters = new { Id = id, paymentMetodCreateDto.MetodName, };

        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[PaymentMetodsUpdate]",
            parameters,
            "DefaultConnection"
        );
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
