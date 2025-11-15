using System.Collections.Generic;

namespace MovieAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
