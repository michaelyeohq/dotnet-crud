using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Products;
using Store.Dtos.Products;
using Store.Helpers;
using Store.Models;

namespace Store.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        public ProductController(IProductRepo repository, IMapper mapper, IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _repository = repository;

        }

        //GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtUtils.ValidateToken(jwt);
                var products = _repository.GetAllProducts();

                return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
            }
            catch (Exception)
            {
                throw new AppException("Unauthorized");
            }
        }

        //GET api/products/{id}
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtUtils.ValidateToken(jwt);
                var product = _repository.GetProductById(id);
                if (product != null)
                {
                    return Ok(_mapper.Map<ProductReadDto>(product));
                }
                return NotFound();
            }
            catch (Exception)
            {

                throw new AppException("Unauthorized");
            }

        }

        //POST api/products
        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto prdt)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtUtils.ValidateToken(jwt);
                var product = _mapper.Map<Product>(prdt);
                _repository.CreateProduct(product);
                _repository.SaveChanges();

                var productReadDto = _mapper.Map<ProductReadDto>(product);

                return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id }, productReadDto);
            }
            catch (Exception)
            {
                throw new AppException("Unauthorized");
            }
        }

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductUpdateDto prdt)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtUtils.ValidateToken(jwt);
                var product = _repository.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                _mapper.Map(prdt, product);

                _repository.UpdateProduct(product);

                _repository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                throw new AppException("Unauthorized");
            }
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtUtils.ValidateToken(jwt);
                var product = _repository.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                _repository.DeleteProduct(product);

                _repository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                throw new AppException("Unauthorized");
            }
        }
    }
}