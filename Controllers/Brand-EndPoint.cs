﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummaryProfileAPI.Models;

namespace SummaryProfileAPI.Controllers
{
    [Route("api/Brand-info")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly BrandContext _dbContext;

        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            return await _dbContext.Brands.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {

            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = _dbContext.Brands.FindAsync(id);



            if (brand == null)
            {
                return NotFound();
            }
            return await brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {


            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand),new { id = brand.ID },brand);
        }
    }
}
