using AutoMapper;
using Store.Dtos.Products;
using Store.Models;

namespace Store.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            // POST api/products
            CreateMap<ProductCreateDto, Product>();
            // GET api/products/{id}
            CreateMap<ProductReadDto, Product>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductUpdateDto, Product>();
        }
        
    }
}