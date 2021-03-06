﻿using CarmackFX.Client.Domain;
using System.Threading.Tasks;

namespace CarmackFX.Client.Security
{
	[Service(ServiceType.Server)]
    public interface ISecurityService
    {
		ServiceTask Auth<T>(T authIn);
    }
}
