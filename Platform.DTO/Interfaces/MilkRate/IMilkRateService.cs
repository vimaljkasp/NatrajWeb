using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO.Interfaces.MilkRate
{
    public interface IMilkRateService
    {
        decimal GetMilkRateByVLCAndFAT_CLR(int VLCId, decimal fat, decimal clr);
        
    }
}
