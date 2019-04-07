using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class NatrajConfigurationDTO
    {
        public int ConfigurationId { get; set; }

        public string KeyData { get; set; }

        public string KeyName { get; set; }

        public string Value { get; set; }
        
        public string DefaultValue { get; set; }

        public string Description { get; set; }


    }
}
