using System;
namespace GrawApp.Database
{
    public class UserStation
    {
        public UserStation()
        {
        }

        public int Id { get; set; }
        public Users User { get; set; }
        public Station Station { get; set; }
        public bool IsDefault { get; set; }
    }
}
