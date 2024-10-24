using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class PaymentStatusCreateDto
{
    [Required(ErrorMessage = "PaymentStatus Name is required")]
    public string? StatusName { get; set; }
}
