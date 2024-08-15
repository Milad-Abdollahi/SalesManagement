using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public UserRoleRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<UserRoleModel?> CreateAsync(UserRoleCreateDto userRoleCreateDto)
    {
        var result = await _dapperDataAccess.LoadData<UserRoleModel, dynamic>(
            "[dbo].[UserRoleInsert]",
            new { RoleName = userRoleCreateDto.RoleName },
            "DefaultConnection"
        );

        return result.FirstOrDefault();
    }

    // Read
    public async Task<List<UserRoleModel?>> GetAllUserRoleModelsAsync()
    {
        return await _dapperDataAccess.LoadData<UserRoleModel>(
            "[dbo].[UserRolesGetAll]",
            "DefaultConnection"
        );
    }

    public async Task<UserRoleModel?> GetUserRoleByIdAsync(int id)
    {
        var result = await _dapperDataAccess.LoadData<UserRoleModel, dynamic>(
            "[dbo].[UserRoleGetById]",
            new { Id = id },
            "DefaultConnection"
        );
        return result.FirstOrDefault();
    }

    // Update
    public Task UpdateUserRoleAsync(int id, UserRoleCreateDto userRoleCreateDto)
    {
        var parameters = new { RoleId = id, RoleName = userRoleCreateDto.RoleName };

        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[UserRoleUpdate]",
            parameters,
            "DefaultConnection"
        );
    }

    // Delete
    public Task DeleteUserRoleAsync(int id)
    {
        return _dapperDataAccess.SaveData<dynamic>(
            "[dbo].[UserRoleDelete]",
            new { RoleId = id },
            "DefaultConnection"
        );
    }
}
