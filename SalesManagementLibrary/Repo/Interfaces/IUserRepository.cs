using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IUserRepository
{
    // Create
    Task<UserModel?> CreateAsync(UserCreateDto userCreateDto);


    // Read
    Task<List<UserModel?>> GetAllUsers();

    Task<UserModel?> GetUserById(int id);



    //Update
    Task UpdateUser(int userId ,UserCreateDto userCreateDto);


    // Delete
}
