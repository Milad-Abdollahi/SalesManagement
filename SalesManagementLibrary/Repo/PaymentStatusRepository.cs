using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class PaymentStatusRepository : IPaymentStatusRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public PaymentStatusRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<PaymentStatusModel?> CreatePaymentStatusAsync(
        PaymentStatusCreateDto paymentStatusCreateDto
    )
    {
        var parameters = new { paymentStatusCreateDto.StatusName, };

        List<PaymentStatusModel> result = await _dapperDataAccess.LoadData<
            PaymentStatusModel,
            dynamic
        >("[dbo].[PaymentStatusInsert]", parameters, "DefaultConnection");

        return result.FirstOrDefault();
    }

    // Read
    public async Task<List<PaymentStatusModel?>> GetAllPaymentStatusesAsync()
    {
        var result = await _dapperDataAccess.LoadData<PaymentStatusModel?>(
            "[dbo].[PaymentStatusesGetAll]",
            "DefaultConnection"
        );

        return result;
    }

    public async Task<PaymentStatusModel?> GetPaymentStatusByIdAsync(int id)
    {
        var result = await _dapperDataAccess.LoadData<PaymentStatusModel?, dynamic>(
            "[dbo].[PaymentStatusesGetById]",
            new { PaymentStatusId = id },
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Update
    public async Task UpdatePaymentStatusAsync(
        int id,
        PaymentStatusCreateDto paymentStatusCreateDto
    )
    {
        var parameter = new
        {
            PaymentStatusId = id,
            StatusName = paymentStatusCreateDto.StatusName,
        };

        try
        {
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[PaymentStatusesUpdate]",
                parameter,
                "DefaultConnection"
            );
        }
        catch (SqlException ex) when (ex.Number == 2627)
        {
            throw new InvalidOperationException(
                $"A payment Status with this name already exists: {paymentStatusCreateDto.StatusName}"
            );
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    // Delete
    public Task DeletePaymentStatusAsync(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[PaymentStatusesDelete]",
            new { PaymentStatusId = id },
            "DefaultConnection"
        );
    }
}
