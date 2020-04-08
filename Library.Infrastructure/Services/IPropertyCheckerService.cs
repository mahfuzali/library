using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Services
{
    public interface IPropertyCheckerService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
