using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Dtos;
using Store.Models;

namespace Store.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        //GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _repository.GetAllProducts();

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        //GET api/products/{id}
        [HttpGet("{id}", Name="GetProductById")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(product));
            }
            return NotFound();
        }

        //POST api/products
        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto prdt)
        {
            var product = _mapper.Map<Product>(prdt);
            _repository.CreateProduct(product);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(product);
            
            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id }, productReadDto);
        }

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductUpdateDto prdt)
        {
            var product = _repository.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }

            _mapper.Map(prdt, product);

            _repository.UpdateProduct(product);

            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _repository.DeleteProduct(product);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}