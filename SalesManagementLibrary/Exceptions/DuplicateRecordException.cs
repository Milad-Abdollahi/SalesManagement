using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.Exceptions;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException(string message)
        : base(message) { }
}
