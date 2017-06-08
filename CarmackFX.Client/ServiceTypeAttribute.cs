using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client
{
    public class ServiceTypeAttribute : Attribute
    {
        public ServiceType Type { get; set; }
    }
}
