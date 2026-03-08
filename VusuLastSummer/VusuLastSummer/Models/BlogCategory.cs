namespace VusuLastSummer.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        // Bir kateqoriyanın çoxlu bloq postu ola bilər
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
