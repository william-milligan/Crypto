using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public interface IUser
    {
        IEnumerable<User> Users { get; }

        User GetUser(int UserID);
    }
}
