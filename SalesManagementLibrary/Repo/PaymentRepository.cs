using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo;

public class PaymentRepository : IPaymentRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public PaymentRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<PaymentModel?> CreatePaymentAsync(PaymentCreateDto paymentCreateDto)
    {
        var parameter = new
        {
            OrderId = paymentCreateDto.OrderId,
            PaymentDate = paymentCreateDto.PaymentDate,
            Amount = paymentCreateDto.Amount,
            PaymentMethodId = paymentCreateDto.PaymentMetod.Id,
            PaymentStatusId = paymentCreateDto.PaymentStatus.PaymentStatusId,
        };
        var result = await _dapperDataAccess
            .LoadData<PaymentModel?, dynamic>("[dbo].[PaymentsInsert]", parameter, "DefaultConnection");
        return result.FirstOrDefault();
    }
    

    // Read
    public Task<List<PaymentModel?>> GetAllPaymentsAsync()
    {
        throw new NotImplementedException();
    }
    public Task<PaymentModel?> GetPaymentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }


    // Update
    public Task UpdatePaymentAsyc(int id)
    {
        throw new NotImplementedException();
    }


    // Delete
    public Task DeletePaymentAsync(int id)
    {
        throw new NotImplementedException();
    }

}
