﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Models;

public class PaymentMethodModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Method Name is required")]
    public string? MethodName { get; set; }
}