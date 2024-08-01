using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Repo;

public class UserRepository : IUserRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public UserRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<UserModel?> CreateAsync(UserCreateDto userCreateDto)
    {
        var parameters = new 
        {
            userCreateDto.Username,
            userCreateDto.PasswordHash,
            userCreateDto.Email,
            userCreateDto.RoleId,
            userCreateDto.CreatedDate,
            userCreateDto.LastLoginDate
        };

        var result = await _dapperDataAccess
            .LoadData<UserModel, dynamic>("dbo.UserInsert",
            parameters,
            "DefaultConnection");

        return result.FirstOrDefault();
    }




    // Read
    public async Task<List<UserModel?>> GetAllUsers()
    {
        return await _dapperDataAccess.LoadData<UserModel, dynamic>("dbo.UsersGetAll", new { }, "DefaultConnection"); 
    }









    // Update


    // Delete
}
