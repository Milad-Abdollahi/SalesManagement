using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.BaseRepo;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class RoleRepository : BaseRepository, IEntityRepository<RoleModel, RoleCreateDto>
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public RoleRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<RoleModel?> CreateAsync(RoleCreateDto createDto)
    {
        return await ExecWithErrHandling<RoleModel?>(async () =>
        {
            var parameters = new { Name = createDto.Name, };
            var results = await _dapperDataAccess.LoadData<RoleModel?, dynamic>(
                "[dbo].[RolesInsert]",
                parameters,
                "DefaultConnection"
            );
            return results.FirstOrDefault();
        });
    }

    // Read

    public async Task<List<RoleModel?>> GetAllAsync()
    {
        return await ExecWithErrHandling<List<RoleModel?>>(async () =>
        {
            var results = await _dapperDataAccess.LoadData<RoleModel?>("[dbo].[RolesGetAll]", "DefaultConnection");
            return results;
        });
    }

    public async Task<RoleModel?> GetByIdAsync(int id)
    {
        return await ExecWithErrHandling<RoleModel?>(async () =>
        {
            var results = await _dapperDataAccess.LoadData<RoleModel?, dynamic>(
                "[dbo].[RolesGetById]",
                new { Id = id },
                "DefaultConnection"
            );
            return results.FirstOrDefault();
        });
    }

    // Update

    public async Task UpdateAsync(int id, RoleCreateDto createDto)
    {
        await ExecWithErrHandling(async () =>
        {
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[RolesUpdate]",
                new { Id = id, Name = createDto.Name },
                "DefaultConnection"
            );
        });
    }

    public async Task DeleteAsync(int id)
    {
        await ExecWithErrHandling(async () =>
        {
            await _dapperDataAccess.SaveData<dynamic>("[dbo].[RolesDelete]", new { Id = id }, "DefaultConnection");
        });
    }
}
