using LoginApp.Utils.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Utils.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetDbPath()
        {
            return ConfigurationManager.AppSettings["DbPath"];
        }

        public string GetDefaultAdminUserName()
        {
            return ConfigurationManager.AppSettings["DefaultAdmin"];
        }

        public string GetDefaultAdminPassword()
        {
            return ConfigurationManager.AppSettings["DefaultPassword"];
        }
    }
}
