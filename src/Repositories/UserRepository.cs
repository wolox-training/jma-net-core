using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using testing_net.Models;
using testing_net.Repositories.Database;
using testing_net.Repositories.Interfaces;

namespace testing_net.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository 
    {
        public UserRepository(DataBaseContext context) : base(context)
        {
        }

    }
}
