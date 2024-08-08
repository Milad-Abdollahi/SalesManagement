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
            PaymentMethodId = paymentCreateDto.PaymentMetod.PaymentMethodId,
            PaymentStatusId = paymentCreateDto.PaymentStatus.PaymentStatusId,
        };
        var result = await _dapperDataAccess
            .LoadData<PaymentModel?, dynamic>("[dbo].[PaymentsInsert]", parameter, "DefaultConnection");
        return result.FirstOrDefault();
    }


    // Read
    




    public async Task<PaymentModel?> GetPaymentByIdAsync(int id)
    {
        var payments = await _dapperDataAccess
            .LoadData<PaymentModel?, dynamic>("[dbo].[PaymentsGetById]", new { PaymentId = id }, "DefaultConnection");

        var payment = payments.FirstOrDefault();

        if (payment is null)
        {
            return null;
        }

        var paymentStatuses = await _dapperDataAccess
            .LoadData<PaymentStatusModel?, dynamic>(
            "[dbo].[PaymentStatusesGetByPaymentId]", 
            new { PaymentId = id },
            "DefaultConnection");

        PaymentStatusModel? paymentStatus = paymentStatuses.FirstOrDefault();

        var paymentMetods = await _dapperDataAccess
            .LoadData<PaymentMethodModel?, dynamic>(
            "[dbo].[PaymentMetodGetByPaymentId]",
            new { PaymentId = id },
            "DefaultConnection"
            );
        PaymentMethodModel? paymentMetod = paymentMetods.FirstOrDefault();


        payment.PaymentStatus = paymentStatus;
        payment.PaymentMetod = paymentMetod;


        return payment;
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

    public async Task<List<PaymentModel?>> GetAllPaymentsAsync()
    {
        //var sql = "[dbo].[PaymentsGetAll]";

        //var payments = await _dapperDataAccess.LoadDataWithMultiMapping<PaymentModel, PaymentStatusModel, PaymentMethodModel, dynamic>(
        //    sql,
        //    (payment, paymentStatus, paymentMethod) =>
        //    {
        //        payment.PaymentStatus = paymentStatus;
        //        payment.PaymentMetod = paymentMethod;
        //        return payment;
        //    },
        //    new { },
        //    "DefaultConnection",
        //    splitOn: "PaymentStatusId,PaymentMethodId"
        //);

        //return payments;


        List<PaymentDto?> paymentDtos = await this.GetAllPaymentsWithDetailsAsync();

        List<PaymentModel> payments = new List<PaymentModel>();

        foreach (var paymentDto in paymentDtos)
        {
            payments.Add(this.MapDtoToModel(paymentDto));
        }

        return payments;
    }


    public async Task<List<PaymentDto?>> GetAllPaymentsWithDetailsAsync()
    {
        var sql = "[dbo].[PaymentsGetAllWithDetails]";

        List<PaymentDto?> payments = await _dapperDataAccess.LoadData<PaymentDto>(
            sql,
            "DefaultConnection"
        );

        return payments;
    }


    public PaymentModel MapDtoToModel(PaymentDto dto) 
    {
        return new PaymentModel
        {
            PaymentId = dto.PaymentId,
            OrderId = dto.OrderId,
            PaymentDate = dto.PaymentDate,
            Amount = dto.Amount,
            PaymentStatus = new PaymentStatusModel 
            { 
                PaymentStatusId = dto.PaymentStatusId ,
                StatusName = dto.StatusName
            },
            PaymentMetod = new PaymentMethodModel
            {
                PaymentMethodId = dto.PaymentMethodId ,
                MethodName = dto.MethodName ,
            }
        };
    }

}
