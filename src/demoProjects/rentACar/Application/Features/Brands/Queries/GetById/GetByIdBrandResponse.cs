using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetById
{
    public class GetByIdBrandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        DateTime? DeleteDate { get; set; }
        DateTime? UpdateeDate { get; set; }
        DateTime? InsertDate { get; set; }
    }
}
