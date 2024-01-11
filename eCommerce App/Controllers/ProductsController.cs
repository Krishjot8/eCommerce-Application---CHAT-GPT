using AutoMapper;
using eCommerce_App.Data;
using eCommerce_App.DTOs;
using eCommerce_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ECommerceContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ECommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {

            var product = _mapper.Map<Product>( productDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();


            var createdProductDto = _mapper.Map<ProductDto>(product);

            // Return the created productDto or another appropriate response
            return CreatedAtAction(nameof(GetProduct), new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto updatedProductDto)
        {
            if (id != updatedProductDto.Id)
            {
                return BadRequest();
            }

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }


            _mapper.Map( updatedProductDto ,existingProduct);

            try
            {


                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (!_context.Products.Any(p => p.Id == id))
                {

                    return NotFound();

                }
                else {



                    throw;
                
                }
           
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productToRemove = await _context.Products.FindAsync(id);

            if (productToRemove == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productToRemove);
            await _context.SaveChangesAsync();

            return NoContent();
        }

     
    }
}
