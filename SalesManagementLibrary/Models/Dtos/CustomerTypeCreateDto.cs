using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class CustomerTypeCreateDto
{
    [Required(ErrorMessage = "Customer Type Name required!")]
    public string? TypeName { get; set; }
}
