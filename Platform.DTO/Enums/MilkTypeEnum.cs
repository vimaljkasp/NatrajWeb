using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public enum MilkTypeEnum
    {
        [Description("None")]
        None,
        [Description("Cow Milk")]
        CowMilk,
        [Description("Buffelo Milk")]
        BuffeloMilk,
        [Description("Mixed Milk")]
        MixedMilk
    }
}
