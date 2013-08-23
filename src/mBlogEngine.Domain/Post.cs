namespace mBlogEngine.Domain
{
    public class Post
    {
	    private readonly Blog _blog;

	    public Post(Blog blog)
	    {
		    _blog = blog;
	    }

	    public string Title { get; private set; }
		
		public string Text { get; private set; }

	    public string Summary { get; private set; }

		public bool Published { get; private set; }

	    public string Decorated
	    {
		    get { return _blog.Writer.Decorated(this); }
	    }

	    public Post SetTitle(string title)
	    {
		    Title = title;
		    return this;
	    }

	    public Post SetText(string text)
	    {
		    Text = text;
		    return this;
	    }

	    public Post SetSummary(string summary)
	    {
		    Summary = summary;
		    return this;
	    }

	    public void Publish()
	    {
		    Published = true;
	    }
    }
}
