using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
          =>await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec)     
         => await ApplySpecification(Spec).ToListAsync();
        

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
         => await ApplySpecification(Spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        =>  SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), Spec);

        public async Task<int> GetCountAsync(ISpecification<T> Spec)
           => await ApplySpecification(Spec).CountAsync();

        
    }
}
