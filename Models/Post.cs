namespace donatETH.Models
{
    public class Post
    {

        public string Id { get; set; } = "";
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string Creator { get; set; }
        public int Price { get; set; }
        public int Goal { get; set; }
        public int CurrentSum { get; set; }
        public int LikeCount { get; set; }

        public int Index { get; set; } = 0;

    }
}
