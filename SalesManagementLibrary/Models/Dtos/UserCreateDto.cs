using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class UserCreateDto
{
    public string? Username { get; set; }

    public string PasswordHash { get; set; }


    public string Email { get; set; }


    public int RoleId { get; set; }


    public DateTime CreatedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }
}
