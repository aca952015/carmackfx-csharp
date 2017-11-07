using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Security
{
    [Service(ServiceType.Server)]
    public interface ISecurityService
    {
        Task<AuthResult> Auth<T>(T authIn);
    }
}
