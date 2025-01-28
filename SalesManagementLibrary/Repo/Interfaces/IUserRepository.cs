using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;

namespace SalesManagementLibrary.Repo.Interfaces;

public interface IUserRepository : IEntityRepository<UserModel, UserCreateDto>
{
    Task<UserModel?> GetUserByUsername(string userName);
    Task AssignRoleToUserAsync(int userId, int roleId);
}
