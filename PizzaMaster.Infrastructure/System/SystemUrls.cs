using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.Infrastructure.System
{
 
    public static class SystemUrls
    {

        public static class User
        {
            public const string prefs = "Users";


            public const string Login = $"{prefs}/Login";
            public const string Logout = $"{prefs}/Register";
            // Add more actions as needed
        }
        // Add more roles as needed
    }
}
