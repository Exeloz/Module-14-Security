using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LoginApp.Model;
using LoginApp.Data.Repositories;
using LoginApp.Utils;
using LoginApp.Utils.Commands;
using LoginApp.Utils.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace LoginApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private readonly ILogger<MainViewModel> _logger;

        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToLoginViewCommand { get; set; }

        public MainViewModel(INavigationService navigationService, ILogger<MainViewModel> logger)
        {           
            _navigationService = navigationService;
            NavigateToLoginViewCommand = new RelayCommand(() => NavigationService.NavigateTo<UserLoginViewModel>());
            NavigationService.NavigateTo<UserLoginViewModel>();
            _logger = logger;

            TesterLog();
        }

        private void TesterLog()
        {
            _logger.LogInformation("Test log : opération normale");
            _logger.LogWarning("Test log : comportement innatendu.");
            _logger.LogError("Test log : erreur!");
        }
    }
}

