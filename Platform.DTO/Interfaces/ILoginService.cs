
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface ILoginService
    {

        LoggedInUserDTO ValidateLoginAndCreateLoginSession(string userName,string password);
    }
}
