using Newtonsoft.Json;
using System;

namespace CarmackFX.Client
{
	public class ServiceResponse
	{
		public bool IsSuccess { get; set; }
		public String Data { get; set; }
		public Exception Error { get; set; }

		public T Get<T>()
		{
			if(string.IsNullOrEmpty(this.Data))
			{
				return default;
			}

			return JsonConvert.DeserializeObject<T>(this.Data);
		}
	}
}
