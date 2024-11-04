using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.BaseRepo;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class PaymentStatusRepository : BaseRepository, IPaymentStatusRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public PaymentStatusRepository(IDapperDataAccess dapperDataAccess)
    //: base(dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<PaymentStatusModel?> CreateAsync(
        PaymentStatusCreateDto paymentStatusCreateDto
    )
    {
        var parameters = new { paymentStatusCreateDto.StatusName };

        return await ExecWithErrHandling<PaymentStatusModel?>(async () =>
        {
            List<PaymentStatusModel?> result = await _dapperDataAccess.LoadData<
                PaymentStatusModel?,
                dynamic
            >("[dbo].[PaymentStatusInsert]", parameters, "DefaultConnection");
            return result?.FirstOrDefault();
        });
    }

    // Read
    public async Task<List<PaymentStatusModel?>> GetAllAsync()
    {
        return await ExecWithErrHandling<List<PaymentStatusModel?>>(async () =>
        {
            var result = await _dapperDataAccess.LoadData<PaymentStatusModel?>(
                "[dbo].[PaymentStatusesGetAll]",
                "DefaultConnection"
            );
            return result;
        });
    }

    public async Task<PaymentStatusModel?> GetByIdAsync(int id)
    {
        return await ExecWithErrHandling<PaymentStatusModel?>(async () =>
        {
            var result = await _dapperDataAccess.LoadData<PaymentStatusModel?, dynamic>(
                "[dbo].[PaymentStatusesGetById]",
                new { PaymentStatusId = id },
                "DefaultConnection"
            );
            return result.FirstOrDefault();
        });
    }

    // Update
    public async Task UpdateAsync(int id, PaymentStatusCreateDto paymentStatusCreateDto)
    {
        await ExecWithErrHandling(async () =>
        {
            var parameter = new
            {
                PaymentStatusId = id,
                StatusName = paymentStatusCreateDto.StatusName,
            };
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[PaymentStatusesUpdate]",
                parameter,
                "DefaultConnection"
            );
        });
    }

    // Delete
    public async Task DeleteAsync(int id)
    {
        await ExecWithErrHandling(async () =>
        {
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[PaymentStatusesDelete]",
                new { PaymentStatusId = id },
                "DefaultConnection"
            );
        });
    }
}
