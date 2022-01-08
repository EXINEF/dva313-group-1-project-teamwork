using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public static class DatabaseCredentials
    {
        //Works for now, might want to anonymize/encrypt this in the future

        public const string IP = "mysql-64846-0.cloudclusters.net";
        public const string PORT = "12457";
        public const string USERNAME = "bob";
        public const string PASSWORD = "MVa5fPoA";
        public const string DATABASE_NAME = "tpmsDB";

        public static void LoadDatabaseCredentials()
        {

        }
    }
}
