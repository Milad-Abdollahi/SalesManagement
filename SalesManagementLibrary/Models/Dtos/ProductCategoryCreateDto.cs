using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class ProductCategoryCreateDto
{
    [Required(ErrorMessage = "CategoryName is required")]
    [MinLength(1, ErrorMessage = "CategoryName cannot be empty")]
    public string? CategoryName { get; set; }
}
