using System;

namespace CarmackFX.Client.Error
{
	public class MethodNotFoundException : Exception
	{
		public string ServiceName { get; private set; }
		public string MethodName { get; private set; }

		public MethodNotFoundException(string serviceName, string methodName)
		{
			this.ServiceName = serviceName;
			this.MethodName = methodName;
		}

		public override string Message => string.Format("{0}.{1} not found.", ServiceName, MethodName);
	}
}
