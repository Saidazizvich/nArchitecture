using Core.Persistance.Repositories;
using Domain.Enums;

namespace Domain.Entity
{
    public class Car : Entity<Guid>
    {


        public Guid ModelId { get; set; }

        public int Kilometr { get; set; }
        public short ModelYear { get; set; }

        public string PlateNumber { get; set; }


        public short MinFindexScore { get; set; } // arabani bulmak yada bankada id olarak bulmak yonteni yani status gibi

        public CarState CarState { get; set; }

        public virtual Model? Model { get; set; }

        public Car() 
        { 
        
        }

        public Car(Guid modelId, int kilometr, short modelYear, string plateNumber, short minFindexScore)
        {
            ModelId = modelId;
            Kilometr = kilometr;
            ModelYear = modelYear;
            PlateNumber = plateNumber;
            MinFindexScore = minFindexScore;
          
        }
    }


}
