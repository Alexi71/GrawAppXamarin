using System;
using SQLite;
using SQLite.Net.Attributes;

namespace GrawApp.Database
{
    public class Users
    {
        public Users()
        {
        }
        [PrimaryKey,AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }
    }
}
