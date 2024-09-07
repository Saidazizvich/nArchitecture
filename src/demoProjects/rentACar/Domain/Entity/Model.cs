using Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Model : Entity<Guid>
    {

        public Guid BrandId { get; set; } // marka

        public Guid FuilId { get; set; } //yakit

        public Guid TranmissionId { get; set; } // 

        public string Name { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImageUrl { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Fuel Fuel { get; set; }
        public virtual Trasmission Trasmission { get; set; }
        public virtual ICollection<Car> Cars { get; set; }


        public Model()
        {
             Cars = new HashSet<Car>(); 
        }

        public Model(Guid brandId, Guid fuilId, Guid tranmissionId, string name, decimal dailyPrice, string imageUrl)
        {
            BrandId = brandId;
            FuilId = fuilId;
            TranmissionId = tranmissionId;
            Name = name;
            DailyPrice = dailyPrice;
            ImageUrl = imageUrl;
        }
    }


}
