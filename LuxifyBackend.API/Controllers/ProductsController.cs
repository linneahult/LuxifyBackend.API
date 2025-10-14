using Luxify.Core;            // Product-modellen
using Luxify.Infrastructure;   // AppDbContext
using LuxifyBackend.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LuxifyBackend.API.DTOs;

namespace Luxify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // => api/products
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }


        //__________________________________________________________________________________________________________________________



        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] string? category, [FromQuery] string? verificationStatus)
        {
            var query = _db.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            if (!string.IsNullOrWhiteSpace(verificationStatus))
                query = query.Where(p => p.VerificationStatus == verificationStatus);

            var items = await query
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Category = p.Category,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    VerificationStatus = p.VerificationStatus
                })
                .ToListAsync();

            return Ok(items);
        }




        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }




        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                ImageUrl = dto.ImageUrl,
                Condition = dto.Condition,
                VerificationStatus = "Pending",
                SellerId = Guid.NewGuid() // MOCK: byt till riktig inloggad användare senare
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            var result = new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Category = product.Category,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                VerificationStatus = product.VerificationStatus
            };

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, result);
        }






        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Title = dto.Title;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Category = dto.Category;
            product.ImageUrl = dto.ImageUrl;
            product.Condition = dto.Condition;

            await _db.SaveChangesAsync();
            return NoContent();
        }




        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

