using static System.Net.Mime.MediaTypeNames;

namespace HerexamenTry.Shared
{
    public static class UserDto
    {
        public class Index
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Picture { get; set; }
           
        }
    }
}