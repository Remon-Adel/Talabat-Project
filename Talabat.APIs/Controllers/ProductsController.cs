using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepo,
            IMapper mapper)
        {
            _productRepo = ProductRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
            _mapper = mapper;
        }

        [HttpGet]//api/Product
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams ProductParams )
        {
            var Spec = new ProductWithBrandAndTypeSpecification(ProductParams);
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(Products);

            var CountSpec = new ProductWithFilterForCountSpecification(ProductParams);
            var Count = await _productRepo.GetCountAsync(CountSpec);

            return Ok(new Pagination<ProductDto>(ProductParams.PageIndex,ProductParams.PageSize,Count,Data));
        }

        [HttpGet("{id}")] //api/Product/id
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(id);

            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);

            if(Product == null)return NotFound(new ApiResponse(404));   
            return Ok(_mapper.Map<Product,ProductDto> (Product));

        }

        [HttpGet("brands")] //api/Product/brands

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
           var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]// //api/Product/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
 
        }

    }
}
