using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class EFLoginUsers :ILoginUser
    {
        ApplicationDbContext context;

        public EFLoginUsers(ApplicationDbContext cnt)
        {
            context = cnt;
        }

        public IEnumerable<LoginUser> Logs => context.Logs;

        public void Login(LoginUser log)
        {

            context.Logs.Add(log);

            context.SaveChanges();
        }
        public void LogOut(LoginUser log)
        {
            context.Logs.Remove(log);

            context.SaveChanges();
        }
    }
}
