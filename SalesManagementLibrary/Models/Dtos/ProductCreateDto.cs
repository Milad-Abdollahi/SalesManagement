using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models.Dtos;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public ProductCategoryModel Category { get; set; } = new ProductCategoryModel();
    public DateTime CreatedDate { get; set; }
}
