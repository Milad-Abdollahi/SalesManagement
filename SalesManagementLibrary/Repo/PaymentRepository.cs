﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

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
            PaymentStatusId = paymentCreateDto.PaymentStatus.Id,
        };
        var result = await _dapperDataAccess.LoadData<PaymentModel?, dynamic>(
            "[dbo].[PaymentsInsert]",
            parameter,
            "DefaultConnection"
        );
        return result.FirstOrDefault();
    }

    // Read

    public async Task<PaymentModel?> GetPaymentByIdAsync(int id)
    {
        var payments = await _dapperDataAccess.LoadData<PaymentModel?, dynamic>(
            "[dbo].[PaymentsGetById]",
            new { PaymentId = id },
            "DefaultConnection"
        );

        var payment = payments.FirstOrDefault();

        if (payment is null)
        {
            return null;
        }

        var paymentStatuses = await _dapperDataAccess.LoadData<PaymentStatusModel?, dynamic>(
            "[dbo].[PaymentStatusesGetByPaymentId]",
            new { PaymentId = id },
            "DefaultConnection"
        );

        PaymentStatusModel? paymentStatus = paymentStatuses.FirstOrDefault();

        var paymentMetods = await _dapperDataAccess.LoadData<PaymentMetodModel?, dynamic>(
            "[dbo].[PaymentMetodGetByPaymentId]",
            new { PaymentId = id },
            "DefaultConnection"
        );
        PaymentMetodModel? paymentMetod = paymentMetods.FirstOrDefault();

        payment.PaymentStatus = paymentStatus;
        payment.PaymentMetod = paymentMetod;

        return payment;
    }

    public async Task<List<PaymentModel?>> GetAllPaymentsAsync()
    {
        List<PaymentDto?> paymentDtos = await this.GetAllPaymentsWithDetailsAsync();

        List<PaymentModel> payments = new List<PaymentModel>();

        foreach (var paymentDto in paymentDtos)
        {
            payments.Add(this.MapDtoToModel(paymentDto));
        }

        return payments;
    }

    private async Task<List<PaymentDto?>> GetAllPaymentsWithDetailsAsync()
    {
        var sql = "[dbo].[PaymentsGetAllWithDetails]";

        List<PaymentDto?> payments = await _dapperDataAccess.LoadData<PaymentDto>(
            sql,
            "DefaultConnection"
        );

        return payments;
    }

    private PaymentModel MapDtoToModel(PaymentDto dto)
    {
        return new PaymentModel
        {
            PaymentId = dto.PaymentId,
            OrderId = dto.OrderId,
            PaymentDate = dto.PaymentDate,
            Amount = dto.Amount,
            PaymentStatus = new PaymentStatusModel
            {
                Id = dto.PaymentStatusId,
                StatusName = dto.StatusName
            },
            PaymentMetod = new PaymentMetodModel
            {
                Id = dto.PaymentMethodId,
                MetodName = dto.MethodName,
            }
        };
    }

    // Update
    public Task UpdatePaymentAsyc(int id, PaymentCreateDto paymentCreateDto)
    {
        var parameter = new
        {
            PaymentId = id,
            OrderId = paymentCreateDto.OrderId,
            PaymentDate = paymentCreateDto.PaymentDate,
            Amount = paymentCreateDto.Amount,
            PaymentMethodId = paymentCreateDto.PaymentMetod.Id,
            PaymentStatusId = paymentCreateDto.PaymentStatus.Id,
        };

        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[PaymentsUpdate]",
            parameter,
            "DefaultConnection"
        );
    }

    // Delete
    public Task DeletePaymentAsync(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[PaymentsDelete]",
            new { PaymentId = id },
            "DefaultConnection"
        );
    }
}