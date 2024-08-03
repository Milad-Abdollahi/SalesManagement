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

public class UserRoleRepository : IUserRoleRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public UserRoleRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    public async Task<UserRoleModel?> CreateAsync(UserRoleCreateDto userRoleCreateDto)
    {
        var result = await _dapperDataAccess
            .LoadData<UserRoleModel, dynamic>("[dbo].[UserRoleInsert]",
            new { RoleName = userRoleCreateDto.RoleName },
            "DefaultConnection");

        return result.FirstOrDefault();
    }
}
