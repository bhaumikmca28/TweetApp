namespace TweetApp.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string TweetMsg { get; set; } = string.Empty;
        public DateTime TweetDate { get; set; }
        public User? User { get; set; }
    }
}
