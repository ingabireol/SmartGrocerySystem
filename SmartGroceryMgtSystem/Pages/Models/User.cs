namespace SmartGroceryMgtSystem.Pages.Models
{
    public class User
    {
        private int userId;
        private string? password;
        private string? email;
        private string? role;
        private string? name;
        public User() { }

        public User(int userId, string? password, string? email, string? role, string? name)
        {
            this.userId = userId;
            this.password = password;
            this.email = email;
            this.role = role;
            this.name = name;
        }

        public int UserId { get => userId; set => userId = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        public string Name { get => name; set => name = value; }
    }
}
