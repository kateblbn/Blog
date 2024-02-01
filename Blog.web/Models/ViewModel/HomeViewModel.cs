using Blog.web.Models.Domain;

namespace Blog.web.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
