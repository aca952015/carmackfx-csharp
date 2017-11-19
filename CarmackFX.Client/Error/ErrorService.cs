using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Error
{
	public interface IErrorService
	{
		void OnError(Exception ex);
	}
}
