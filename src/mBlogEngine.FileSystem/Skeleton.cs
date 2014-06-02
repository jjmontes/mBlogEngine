using System.IO;

namespace mBlogEngine.FileSystem
{
	public class Skeleton
	{
		public void Init()
		{
			Directory.CreateDirectory("blog");
			Directory.CreateDirectory(@"blog\posts");
		}

		public void InitPost(string postName)
		{
			Directory.CreateDirectory(string.Format(@"blog\posts\{0}", postName));
		}
	}
}
