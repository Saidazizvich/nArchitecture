using Core.Persistance.Repositories;

namespace Domain.Entity
{
    public class Fuel : Entity<Guid> 
    {
        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }

        public Fuel()
        {
           Models = new HashSet<Model>();    
        }

        public Fuel(Guid id, string name )
        {
            Name = name;
            Id = id;
        }
    }


}
