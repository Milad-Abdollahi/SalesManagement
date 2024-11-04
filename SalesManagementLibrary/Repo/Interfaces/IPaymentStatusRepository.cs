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
    Task<PaymentStatusModel?> CreateAsync(PaymentStatusCreateDto paymentStatusCreateDto);

    // Read
    Task<List<PaymentStatusModel?>> GetAllAsync();
    Task<PaymentStatusModel?> GetByIdAsync(int id);

    // Update
    Task UpdateAsync(int id, PaymentStatusCreateDto paymentStatusCreateDto);

    // Delete
    Task DeleteAsync(int id);
}
