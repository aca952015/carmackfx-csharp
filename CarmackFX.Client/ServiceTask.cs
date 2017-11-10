using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client
{
	public class ServiceTask : Task<ServiceResponse>
	{
		public ServiceTask(Func<ServiceResponse> func)
			: base(func)
		{

		}
	}
}
