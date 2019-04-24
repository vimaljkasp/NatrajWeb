using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface INatrajConfigurationService
    {
        ResponseDTO GetAllNatarjConfigurations();

        ResponseDTO UpdateNatrajConfiguration(NatrajConfigurationDTO natrajConfigurationDTO);
    }
}
