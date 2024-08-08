using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public int PaymentStatusId { get; set; }
    public string? StatusName { get; set; }
    public int PaymentMethodId { get; set; }
    public string? MethodName { get; set; }
}
