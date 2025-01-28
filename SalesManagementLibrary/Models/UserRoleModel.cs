namespace SalesManagementLibrary.Models;

public class UserRoleModel
{
    //[Key]
    public int UserId { get; set; }

    //[Required]
    //[StringLength(50)]
    public string RoleId { get; set; }
}
