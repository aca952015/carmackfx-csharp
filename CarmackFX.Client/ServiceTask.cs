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

		public T GetResponse<T>()
		{
			if(this.IsCompleted && Result != null && Result.IsSuccess)
			{
				return Result.Get<T>();
			}

			return default(T);
		}
	}
}
