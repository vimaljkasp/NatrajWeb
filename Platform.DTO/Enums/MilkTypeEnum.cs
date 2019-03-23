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
        N,
        [Description("Cow Milk")]
        C,
        [Description("Buffelo Milk")]
        B,
        [Description("Mixed Milk")]
        M
    }
}
