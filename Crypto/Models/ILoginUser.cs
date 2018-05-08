using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public interface ILoginUser
    {
        IEnumerable<LoginUser> Logs {get;}

        void Login(LoginUser loki);

        void LogOut(LoginUser loki);
    }
}
