using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models;

public class UserModel
{
    //[Key]
    public int Id { get; set; }


    //[Required]
    //[StringLength(50)]
    public string? Username { get; set; }


    //[Required]
    [StringLength(256)]
    public string PasswordHash { get; set; }


    //[Required]
    //[StringLength(100)]
    public string Email { get; set; }


    //[Required]
    public int RoleId { get; set; }


    //[Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }
}
