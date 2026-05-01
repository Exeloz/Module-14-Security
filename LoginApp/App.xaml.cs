using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using LoginApp.Utils.Services.Interfaces;
using LoginApp.Utils.Services;
using LoginApp.ViewModel;
using LoginApp.Model;
using LoginApp.Utils;
using LoginApp.Data.Repositories;
using LoginApp.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog;

namespace LoginApp
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            IConfigurationService configurationService = new ConfigurationService();

            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(configurationService.GetLogConfigPath());
            LogManager.Configuration.Variables["logFilePath"] = Environment.ExpandEnvironmentVariables(configurationService.GetLogFilePath());
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<UserLoginViewModel>();
            services.AddSingleton<WelcomeViewModel>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider =>
            {
                BaseViewModel ViewModelFactory(Type viewModelType)
                {
                    return (BaseViewModel)serviceProvider.GetRequiredService(viewModelType);
                }
                return ViewModelFactory;
            });

            services.AddDbContext<ApplicationDbContext>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Database.EnsureCreated())
                {
                    dbContext.SeedData();
                }
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            this.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"Une erreur non gérée s'est produite.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };

            base.OnStartup(e);
        }
    }
}
