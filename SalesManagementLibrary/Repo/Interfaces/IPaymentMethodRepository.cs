using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IPaymentMethodRepository
{
    // Create
    Task<PaymentMethodModel?> CreatePaymentMethodAsync(PaymentMethodCreateDto paymentMethodCreateDto);

    // Read
    Task<List<PaymentMethodModel?>> GetAllPaymentMethodsAsync();
    Task<PaymentMethodModel?> GetPaymentMethodByIdAsync(int id);

    // Update
    Task UpdatePaymentMethodAsync(int id, PaymentMethodCreateDto paymentMethodCreateDto);

    // Delete
    Task DeletePaymentMethodAsync(int id);
}
