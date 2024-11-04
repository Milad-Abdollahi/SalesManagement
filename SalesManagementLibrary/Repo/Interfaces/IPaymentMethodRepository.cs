using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IPaymentMethodRepository
{
    // Create
    Task<PaymentMethodModel?> CreateAsync(PaymentMethodCreateDto paymentMethodCreateDto);

    // Read
    Task<List<PaymentMethodModel?>> GetAllAsync();
    Task<PaymentMethodModel?> GetByIdAsync(int id);

    // Update
    Task UpdateAsync(int id, PaymentMethodCreateDto paymentMethodCreateDto);

    // Delete
    Task DeleteAsync(int id);
}
