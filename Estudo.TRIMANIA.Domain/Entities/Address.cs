namespace Estudo.TRIMANIA.Domain.Entities
{
    public class Address : EntityBase
    {
        public int UserId { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string? Street { get; private set; }
        public string? Number { get; private set; }
        public string? Neighborhood { get; private set; }

        public Address() { }

        public Address(string? city, string? state, string? street, string? number, string? neighborhood)
        {
            City = city;
            State = state;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
        }
    }
}
