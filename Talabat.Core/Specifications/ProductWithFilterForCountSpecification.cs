using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams ProductParams)
           : base(P =>
              (string.IsNullOrEmpty(ProductParams.Search) || P.Name.ToLower().Contains(ProductParams.Search)) &&
              (!ProductParams.BrandId.HasValue || P.ProductBrandId == ProductParams.BrandId.Value) &&
              (!ProductParams.TypeId.HasValue || P.ProductTypeId == ProductParams.TypeId.Value)


           )
        {

        }
    }
}
