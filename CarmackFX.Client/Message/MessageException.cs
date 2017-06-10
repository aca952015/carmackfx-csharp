using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Message
{
	public class MessageException : Exception
	{
		public ExceptionCode Code { get; set; }

		public MessageException(ExceptionCode code)
		{
			this.Code = code;
		}
	}
}
