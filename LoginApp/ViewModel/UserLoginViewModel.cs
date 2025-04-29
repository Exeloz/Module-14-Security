using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginApp.Utils.Commands;
using LoginApp.Utils.Services.Interfaces;
using System.Windows.Input;
using LoginApp.Utils;
using System.Windows;

namespace LoginApp.ViewModel
{
    internal class UserLoginViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;


        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public UserLoginViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        public ICommand LoginCommand { get; set; }

        private void Login()
        {
            ErrorMessage = string.Empty;

            try
            {
                bool isValidUser = _userService.Login(Email, Password);
                if (isValidUser)
                {
                    _navigationService.NavigateTo<WelcomeViewModel>();

                }
                else
                {
                    ErrorMessage = "Mot de passe ou email non valide";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite pendant la connexion.");
            }

        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
    }
}
