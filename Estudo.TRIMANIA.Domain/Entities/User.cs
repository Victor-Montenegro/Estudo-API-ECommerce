using Estudo.TRIMANIA.Domain.Interfaces;

namespace Estudo.TRIMANIA.Domain.Entities
{
    public class User : EntityBase, IAggregateRoot
    {
        public string? CPF { get; private set; }
        public string? Name { get; private set; }
        public string? Login { get; private set; }
        public string? Email { get; private set; }
        public Address? Address { get; private set; }
        public string? Password { get; private set; }
        public DateTime Birthday { get; private set; }
        public Guid Identification { get; private set; }

        public User()
        {

        }

        public User(string? cPF, string? name, string? login, string? email, DateTime birthday)
        {
            CPF = cPF;
            Name = name;
            Login = login;
            Email = email;
            Birthday = birthday;
            Identification = Guid.NewGuid();
        }

        public void SetPassworld(string? password)
        {
            Password = password;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }

        public void InformationUpdate(string? name, DateTime birthday, string? email)
        {
            Name = name;
            Email = email;
            Birthday = birthday;
        }
    }
}
