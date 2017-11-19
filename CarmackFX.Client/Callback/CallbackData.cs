using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Callback
{
	class CallbackData
	{
		public string ServiceName { get; set; }
		public string MethodName { get; set; }
		public string[] Args { get; set; }
	}
}
