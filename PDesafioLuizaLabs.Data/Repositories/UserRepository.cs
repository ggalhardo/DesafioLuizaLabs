using DesafioLuizaLabs.Domain.Entities;
using DesafioLuizaLabs.Infra.Data.Context;
using PDesafioLuizaLabs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDesafioLuizaLabs.Data.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext context) 
            : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }
    }
}
