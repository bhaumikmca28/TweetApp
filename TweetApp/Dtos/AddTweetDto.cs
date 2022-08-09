namespace TweetApp.Dtos
{
    public class AddTweetDto
    {
        public string TweetMsg { get; set; } = string.Empty;
        public DateTime TweetDate { get; set; }
        public int UserId { get; set; } 
    }
}
