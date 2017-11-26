using System;

namespace CarmackFX.Client.Message
{
	public class MessageException : Exception
	{
		public ExceptionCode Code { get; set; }

		public MessageException(ExceptionCode code)
			: this(code, code.ToString())
		{
		}

		public MessageException(ExceptionCode code, string message)
			: base(message)
		{
			this.Code = code;
		}
	}
}
