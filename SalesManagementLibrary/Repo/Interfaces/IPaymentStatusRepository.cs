using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IPaymentStatusRepository
{
    // Create
    Task<PaymentStatusModel?> CreatePaymentStatusAsync(
        PaymentStatusCreateDto paymentStatusCreateDto
    );

    // Read
    Task<List<PaymentStatusModel?>> GetAllPaymentStatusesAsync();
    Task<PaymentStatusModel?> GetPaymentStatusByIdAsync(int id);

    // Update
    Task UpdatePaymentStatusAsync(int id, PaymentStatusCreateDto paymentStatusCreateDto);

    // Delete
    Task DeletePaymentStatusAsync(int id);
}
