using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickUpBot.Helpers
{
    static class CfgHelper
    {
        public static string tgToken = ConfigurationManager.AppSettings["Token"]!;
        public static string clickupId = ConfigurationManager.AppSettings["ClientId"]!;
        public static string clickupSecret = ConfigurationManager.AppSettings["ClientSecret"]!;
        public static string redirectURL = ConfigurationManager.AppSettings["RedirectURL"]!;
        public static string connectionString = ConfigurationManager.AppSettings["ConnectionString"]!;
        public static string authUrl = $"https://app.clickup.com/api?client_id={clickupId}&redirect_uri={redirectURL}";
    }
}
