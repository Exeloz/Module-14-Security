using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LoginApp.Data.Repositories.Interfaces;
using LoginApp.Model;
using LoginApp.Utils.Services.Interfaces;

namespace LoginApp.Utils.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private User? _currentUser;

        public User? CurrentUser
        {
            get { return _currentUser; }
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string email, string password)
        {

            User? user = _userRepository.GetByEmail(email);

            if (user is null)
                return false;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid)
                return false;

            _currentUser = user;
            return true;
        }

    }
}
