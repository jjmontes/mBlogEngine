namespace mBlogEngine.Domain
{
    public class Post
    {
		public Post(string title)
	    {
		    Title = title;
	    }

	    public string Title { get; set; }
		
		public string Text { get; set; }

	    public string Summary { get; set; }
    }
}
