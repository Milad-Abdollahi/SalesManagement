using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface ICustomerTypeRepository
{
    // Create
    Task<CustomerTypeModel?> CreateCustomerTypeAsync(CustomerTypeCreateDto customerTypeCreateDto);

    // Reade
    Task<List<CustomerTypeModel?>> GetAllCustomerTypesAsync();
    Task<CustomerTypeModel?> GetCustomerTypeByIdAsync(int id);

    // Update
    Task UpdateCustomerTypeAsync(int Id, CustomerTypeCreateDto customerTypeCreateDto);

    // Delete
    Task DeleteCustomerTypeAsync(int Id);
}
