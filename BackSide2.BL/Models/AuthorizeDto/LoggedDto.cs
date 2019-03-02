namespace Auga.BL.Models.AuthorizeDto
{
    public class LoggedDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public long Id { get; set; }
    }
}