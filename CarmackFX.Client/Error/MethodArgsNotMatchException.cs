using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Error
{
	public class MethodArgsNotMatchException : Exception
	{
		public string ServiceName { get; private set; }
		public string MethodName { get; private set; }

		public MethodArgsNotMatchException(string serviceName, string methodName)
		{
			this.ServiceName = serviceName;
			this.MethodName = methodName;
		}

		public override string Message => string.Format("{0}.{1} not found.", ServiceName, MethodName);
	}
}
