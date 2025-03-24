namespace shop_backend.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
