using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class EFUsers : IUser
    {
        private ApplicationDbContext context;

        public EFUsers(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<User> Users => context.Us;

        public User GetUser(int acountID)
        {
            foreach (User f in Users)
            {
                if (f.AcountID == acountID)
                    return f;
            }
            return null;
        }
    }
}
