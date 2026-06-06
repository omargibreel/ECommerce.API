using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Implementation.Exceptions
{
    public sealed class NotFoundException:Exception
    {
        public NotFoundException(string entity , object key):base($"{entity} with key '{key}' was not found.")
        {
            
        }
    }
}
