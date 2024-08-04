using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IUserRoleRepository
{
    // Create
    Task<UserRoleModel?> CreateAsync(UserRoleCreateDto userRoleCreateDto);

    // Read
    Task<List<UserRoleModel?>> GetAllUserRoleModelsAsync();

    Task<UserRoleModel?> GetUserRoleByIdAsync(int id);

    // Update

    Task UpdateUserRoleAsync(int id, UserRoleCreateDto userRoleCreateDto);


    // Delete
    Task DeleteUserRoleAsync(int id);


}
