using System;
using System.Collections.Generic;
using System.Text;

namespace OrderReadService.Application.Ports
{
    public interface ICacheService
    {
        Task RemoveAsync(string key);
    }
}
