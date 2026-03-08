using VusuLastSummer.Models;


namespace VusuLastSummer.ViewModels.Home
{
    public class HomeVM
    {
        // ana səhifədə sadəcə 3 dənə önə çıxan məhsulu göstərəcəyik
        public IEnumerable<VusuLastSummer.Models.Product> FeaturedProducts { get; set; } = new List<VusuLastSummer.Models.Product>();
    }
}
