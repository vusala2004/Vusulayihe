using VusuLastSummer.Models;

namespace VusuLastSummer.ViewModels.Blog
{
    public class BlogIndexVM
    {
        public IEnumerable<BlogCategory> Categories { get; set; } = new List<BlogCategory>();
        public IEnumerable<BlogPost> Posts { get; set; } = new List<BlogPost>();
    }
}
