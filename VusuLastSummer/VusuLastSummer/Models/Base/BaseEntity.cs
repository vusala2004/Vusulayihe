namespace VusuLastSummer.Models.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
    }
}
