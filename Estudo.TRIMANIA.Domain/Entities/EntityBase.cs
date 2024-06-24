namespace Estudo.TRIMANIA.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime creation_date { get; set; }
    }
}
