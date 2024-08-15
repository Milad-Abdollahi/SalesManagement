using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IUserRepository
{
    // Create
    Task<UserModel?> CreateAsync(UserCreateDto userCreateDto);

    // Read
    Task<List<UserModel?>> GetAllUsers();

    Task<UserModel?> GetUserById(int id);

    Task<UserModel?> GetUserByUsername(string userName);

    //Update
    Task UpdateUser(int userId, UserCreateDto userCreateDto);

    // Delete
    Task DeleteUser(int userId);
}
