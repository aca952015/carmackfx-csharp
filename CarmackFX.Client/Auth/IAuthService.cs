using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Auth
{
    [ServiceType(Type = ServiceType.Auth)]
    public interface IAuthService
    {
        Task<AuthResult> Verify<T>(T authIn);
    }
}
