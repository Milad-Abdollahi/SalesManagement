using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IPaymentMetodRepository
{
    // Create
    Task<PaymentMetodModel?> CreatePaymentMetodAsync(PaymentMetodCreateDto paymentMethodCreateDto);

    // Read
    Task<List<PaymentMetodModel?>> GetAllPaymentMetodsAsync();
    Task<PaymentMetodModel?> GetPaymentMetodByIdAsync(int id);

    // Update
    Task UpdatePaymentMetodAsync(int id, PaymentMetodCreateDto paymentMethodCreateDto);

    // Delete
    Task DeletePaymentMetodAsync(int id);
}
