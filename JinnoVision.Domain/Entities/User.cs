namespace JinnoVision.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        // store hashed password in real scenario
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
