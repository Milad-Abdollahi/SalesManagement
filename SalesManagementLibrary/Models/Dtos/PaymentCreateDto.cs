using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class PaymentCreateDto
{
    public int OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatusModel PaymentStatus { get; set; }
    public PaymentMetodModel PaymentMetod { get; set; }
}
