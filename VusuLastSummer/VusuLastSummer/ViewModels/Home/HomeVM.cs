using VusuLastSummer.Models;

namespace VusuLastSummer.ViewModels.Home
{
    public class HomeVM
    {
        // Ana səhifədə sadəcə 3 dənə önə çıxan məhsulu göstərəcəyik
        public IEnumerable<Product> FeaturedProducts { get; set; } = new List<Product>();
    }
}
