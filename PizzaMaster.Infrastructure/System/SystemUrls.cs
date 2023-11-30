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
            public const string Register = $"{prefs}/Register";
            // Add more actions as needed
        }

        public static class Admin
        {
            private const string prefs = "Admin";

            public const string SetAdminData = $"{prefs}/SetAdminData";

            public const string AddVideo = $"{prefs}/AddVideo";

        }
        // Add more roles as needed
    }
}
