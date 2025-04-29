using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginApp.Model;

namespace LoginApp.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User? GetByEmail(string email);
    }
}
