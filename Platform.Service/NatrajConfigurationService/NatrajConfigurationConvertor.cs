using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
   public static class NatrajConfigurationConvertor
    {

        public static NatrajConfigurationDTO ConvertToNatrajConfigurationDTO(NatrajConfiguration natrajConfiguration)
        {
            NatrajConfigurationDTO natrajConfigurationDTO = new NatrajConfigurationDTO();
            natrajConfigurationDTO.ConfigurationId = natrajConfiguration.Id;
            natrajConfiguration.KeyData = natrajConfiguration.KeyData;
            natrajConfiguration.KeyName = natrajConfiguration.KeyName;
            natrajConfiguration.DataVal = natrajConfiguration.DataVal;
            natrajConfiguration.DefaultVal = natrajConfiguration.DefaultVal;
            natrajConfiguration.Description = natrajConfiguration.Description;
            return natrajConfigurationDTO;

        }
    }
}
