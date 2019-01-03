using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
   public class DistributionCenterConvertor
    {
        public static DistributionCenterDTO ConvertToDistributionCenterDto(DistributionCenter distributionCenter)
        {
            DistributionCenterDTO distributionCenterDTO = new DistributionCenterDTO();
            return distributionCenterDTO;
        }

        public static void ConvertToDistributionCenterEntity(ref DistributionCenter distributionCenter, DistributionCenterDTO distributionCenterDTO, bool isUpdate)
        {

        }
    }
}
