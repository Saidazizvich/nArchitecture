using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.CQRS.Create
{
    public class CreateBrandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
