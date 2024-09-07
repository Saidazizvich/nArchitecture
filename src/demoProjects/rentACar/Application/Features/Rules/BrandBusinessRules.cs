using Application.Features.Brands.Constans;
using Application.Service.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rules
{
    public class BrandBusinessRules: BaseBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        // burda simdi bas harf Buyuk olursa bizim kayd etimiz veri biza ela geltiracak ve uni ustunden test yapacak 
         // ve bular exception olarak hata firlaticak

        public async Task BrandNameCannotBeDuplecatedWhenInserted(string name)
        {
            Brand? brand = await _brandRepository.GetAsync(perdicate: b=>b.Name.ToLower()==name.ToLower());

           

            if (brand != null)
            {
                throw new BusinessException(BrandMessages.BrandNameExists);
            } // simdi burda esa ayni kayit varsa biza hata verecak
        }
    }

     // reference tyoe = bu null dir yani icollection yada Ilist IEnumerable 
     // value type esa = int double float long   
}
