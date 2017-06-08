using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Services
{
    [ServiceType(Type = ServiceType.Auth)]
    public interface IAuthService<T>
    {
        AuthResult Verify(T authIn);
    }
}
