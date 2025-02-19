using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    internal class SpecificationEvaluator<TEntity>where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> Spec)
        {
            var Query = InputQuery;// await _Context.Set<Product>()

            if(Spec.Criteria != null)
                Query=Query.Where(Spec.Criteria);//Where(P => P.Id == 10)
            // await _Context.Set<Product>().Where(P => P.Id == 10)

            if(Spec.OrderBy !=null)
                Query=Query.OrderBy(Spec.OrderBy);

            if(Spec.OrderByDescending != null)
                Query=Query.OrderByDescending(Spec.OrderByDescending);

            if (Spec.IsPaginationEnable)
                Query = Query.Take(Spec.Take).Skip(Spec.Skip);

            Query = Spec.Includes.Aggregate(Query, (Currentquery, include) => Currentquery.Include(include));
        // await _Context.Set<Product>().Where(P => P.Id == 10).Include(P => P.ProductBrand)
 // await _Context.Set<Product>().Where(P => P.Id == 10).Include(P => P.ProductBrand).Include(P=> P.ProductType)


            return Query;
        }
    }
}
