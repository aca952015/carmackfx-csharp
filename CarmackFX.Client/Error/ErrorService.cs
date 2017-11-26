using System;

namespace CarmackFX.Client.Error
{
	public interface IErrorService
	{
		void OnError(Exception ex);
	}
}
