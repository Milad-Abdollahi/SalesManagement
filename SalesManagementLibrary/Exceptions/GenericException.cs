using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Exceptions;

public class GenericException : Exception
{
    public GenericException(string message, Exception innerException)
        : base(message, innerException) { }
}
