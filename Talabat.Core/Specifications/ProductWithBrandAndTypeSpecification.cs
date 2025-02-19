using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification :BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams ProductParams)
            :base(P => 
                   (string.IsNullOrEmpty(ProductParams.Search) || P.Name.ToLower().Contains(ProductParams.Search)) &&
                  (!ProductParams.BrandId.HasValue || P.ProductBrandId == ProductParams.BrandId.Value) &&
                  (!ProductParams.TypeId.HasValue || P.ProductTypeId == ProductParams.TypeId.Value)
            
            
            )
        {
          
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);
            AddOrderBy(P => P.Name);

            ApplyPagination(ProductParams.PageSize * (ProductParams.PageIndex - 1), ProductParams.PageSize);

            if(!string.IsNullOrEmpty(ProductParams.Sort))
            {
                switch (ProductParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescendind(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

        }
        public ProductWithBrandAndTypeSpecification(int id):base(P => P.Id == id) // Criteria
        {

            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);

        }

    }
}
