using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IPaymentRepository
{
    // Create
    Task<PaymentModel?> CreatePaymentAsync(PaymentCreateDto paymentCreateDto);

    // Read
    Task<List<PaymentModel?>> GetAllPaymentsAsync();
    Task<PaymentModel?> GetPaymentByIdAsync(int id);

    // Update
    Task UpdatePaymentAsyc(int id, PaymentCreateDto paymentCreateDto);

    // Delete
    Task DeletePaymentAsync(int id);
}
