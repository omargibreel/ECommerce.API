using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Domain.Contracts
{
    public interface IDataInitializer
    {
        Task InitializeAsync();
    }
}
