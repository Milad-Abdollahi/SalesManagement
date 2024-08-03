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
    Task<UserRoleModel?> CreateAsync(UserRoleCreateDto userRoleCreateDto);
}
