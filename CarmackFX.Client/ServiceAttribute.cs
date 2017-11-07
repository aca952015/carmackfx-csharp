using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client
{
    public class ServiceAttribute : Attribute
    {
        public ServiceType ServiceType { get; private set; }

        public ServiceAttribute(ServiceType type)
        {
            this.ServiceType = type;
        }
    }
}
