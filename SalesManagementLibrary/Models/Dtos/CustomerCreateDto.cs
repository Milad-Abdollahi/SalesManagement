using System.ComponentModel.DataAnnotations;

namespace SalesManagementLibrary.Models.Dtos;

public class CustomerCreateDto
{
    [Required(ErrorMessage = "Customer Name is required!")]
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    // Todo**: Change this class later so it only includes CustomerTypeId instead of CustomerTypeModel
    public int CustomerType_id { get; set; }
    public DateTime CreatedDate { get; set; }
}
