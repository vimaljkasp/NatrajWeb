using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Utilities.ExceptionHandler
{
    public class PlatformModuleException : Exception
    {
        public PlatformModuleException()
        {
        }

        public PlatformModuleException(string message)
            : base(message)
        {
        }

        public PlatformModuleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
