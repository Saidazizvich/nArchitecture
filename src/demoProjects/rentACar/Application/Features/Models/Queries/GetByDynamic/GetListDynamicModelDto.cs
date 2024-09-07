using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetByDynamic
{
    public class GetListDynamicModelDto 
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; } // marka

        public string FuelName { get; set; } //yakit

        public string TranmissionName { get; set; } // 

        public string Name { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
