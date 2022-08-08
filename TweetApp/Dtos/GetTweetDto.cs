namespace TweetApp.Dtos
{
    public class GetTweetDto
    {
        public int Id { get; set; }
        public string TweetMsg { get; set; } = string.Empty;
        public DateTime TweetDate { get; set; }
    }
}
