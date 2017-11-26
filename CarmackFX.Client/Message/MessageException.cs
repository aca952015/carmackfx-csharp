using System;

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
