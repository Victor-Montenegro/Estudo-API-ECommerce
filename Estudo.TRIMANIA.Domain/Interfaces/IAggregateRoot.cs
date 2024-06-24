using Estudo.TRIMANIA.Domain.Entities;

namespace Estudo.TRIMANIA.Domain.Interfaces
{
    public interface IAggregateRoot 
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
