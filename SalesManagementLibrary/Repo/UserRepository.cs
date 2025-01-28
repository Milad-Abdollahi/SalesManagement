using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.BaseRepo;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public UserRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<UserModel?> CreateAsync(UserCreateDto userCreateDto)
    {
        return await ExecWithErrHandling<UserModel?>(async () =>
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

            var parameters = new
            {
                userCreateDto.Username,
                PasswordHash = hashedPassword,
                userCreateDto.Email,
                userCreateDto.CreatedDate,
                userCreateDto.LastLoginDate
            };

            var result = await _dapperDataAccess.LoadData<UserModel, dynamic>(
                "dbo.UserInsert",
                parameters,
                "DefaultConnection"
            );

            return result.FirstOrDefault();
        });
    }

    // Read
    public async Task<List<UserModel?>> GetAllAsync()
    {
        return await this.ExecWithErrHandling<List<UserModel?>>(async () =>
        {
            return await _dapperDataAccess.LoadData<UserModel>("dbo.UsersGetAll", "DefaultConnection");
        });
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        return await ExecWithErrHandling(async () =>
        {
            var results = await _dapperDataAccess.LoadData<UserModel, dynamic>(
                "[dbo].[UsersGetById]",
                new { Id = id },
                "DefaultConnection"
            );

            var user = results.FirstOrDefault();

            if (user == null)
            {
                throw new KeyNotFoundException($"User with Id {id} not found.");
            }

            return results.FirstOrDefault();
        });
    }

    public async Task<UserModel?> GetUserByUsername(string userName)
    {
        return await ExecWithErrHandling<UserModel?>(async () =>
        {
            var results = await _dapperDataAccess.LoadData<UserModel?, dynamic>(
                "[dbo].[UsersGetByUsername]",
                new { UserName = userName },
                "DefaultConnection"
            );
            return results.FirstOrDefault();
        });
    }

    // Update
    public async Task UpdateAsync(int userId, UserCreateDto userCreateDto)
    {
        await ExecWithErrHandling(async () =>
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            var parameters = new
            {
                Id = userId,
                userCreateDto.Username,
                PasswordHash = hashedPassword,
                userCreateDto.Email,
                userCreateDto.CreatedDate,
                userCreateDto.LastLoginDate
            };

            await _dapperDataAccess.SaveData<dynamic>("[dbo].[UserUpdate]", parameters, "DefaultConnection");
        });
    }

    public async Task AssignRoleToUserAsync(int userId, int roleId)
    {
        await ExecWithErrHandling(async () =>
        {
            var parameters = new { UserId = userId, RoleId = roleId };
            await _dapperDataAccess.SaveData<dynamic>("[dbo].[UserAssignRole]", parameters, "DefaultConnection");
        });
    }

    // Delete
    public async Task DeleteAsync(int Id)
    {
        await ExecWithErrHandling(async () =>
        {
            await _dapperDataAccess.SaveData<dynamic>("[dbo].[UsersDelete]", new { Id }, "DefaultConnection");
        });
    }
}
