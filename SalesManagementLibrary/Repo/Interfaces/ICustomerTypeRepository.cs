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
    Task<CustomerTypeModel?> CreateAsync(CustomerTypeCreateDto customerTypeCreateDto);

    // Reade
    Task<List<CustomerTypeModel?>> GetAllAsync();
    Task<CustomerTypeModel?> GetByIdAsync(int id);

    // Update
    Task UpdateAsync(int Id, CustomerTypeCreateDto customerTypeCreateDto);

    // Delete
    Task DeleteAsync(int Id);

}
