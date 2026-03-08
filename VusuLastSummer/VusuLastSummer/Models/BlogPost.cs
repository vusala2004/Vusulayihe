namespace VusuLastSummer.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Excerpt { get; set; } // Qısa məzmun (kartda göstərmək üçün)
        public string? ImageUrl { get; set; }

        // Kateqoriya ilə əlaqə (Foreign Key)
        public int BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; } = null!;

        public string AuthorId { get; set; } = string.Empty;
        //public virtual ApplicationUser Author { get; set; } = null!;

        public bool IsPublished { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
