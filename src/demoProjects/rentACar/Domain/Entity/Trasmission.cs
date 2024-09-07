using Core.Persistance.Repositories;

namespace Domain.Entity
{
    public class Trasmission : Entity<Guid> 
    {
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }

        public Trasmission() 
        {
            Models = new HashSet<Model>();  
        }

        public Trasmission(Guid id, string name) : this() 
        {
            Name = name;
                Id = id;
        }
    }


}
