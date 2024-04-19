using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelongeaBoulangerie.DataContext.Models;
using BelongeaBoulangerie.DataContext.Utils;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using BelongeaBoulangerie.DataContext.Migrations;

namespace BelongeaBoulangerie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreadsController : ControllerBase
    {
        private readonly BoulangerieContext _context;
        private readonly BreadService _breadService;

        public BreadsController(BoulangerieContext context, BreadService breadService)
        {
            _breadService = breadService;
            _context = context;
        }

        // GET: api/Breads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bread>>> GetBreads()
        {
            //return await _breadService.AllBreadWithRecipeInfo(); // I want to abstract...
            return await _context.Breads.Include(b => b.Recipe)
                                            .ThenInclude(r => r.Ingredients)
                                        .Include(b => b.Recipe)
                                            .ThenInclude(r => r.Instructions)
                                        .ToListAsync();
        }

        // GET: api/Breads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bread>> GetBread(int id)
        {
            var bread = await _context.Breads.FindAsync(id);

            if (bread == null)
            {
                return NotFound();
            }

            return bread;
        }

        // PUT: api/Breads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBread(int id, Bread bread)
        //{
        //    if (id != bread.BreadId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(bread).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BreadExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Breads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bread>> PostBread(Bread bread)
        {
            _breadService.CreateBread(bread);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBreads", new { id = bread.BreadId}, bread);
            //var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == bread.CountryName);
            //if (country == null)
            //{
            //    return BadRequest("Country not found");
            //}
            //var breadId = _breadService.CreateBreadFromDTO(bread);

            //bread.CountryID = country.CountryId;
            //_context.Breads.Add(bread);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction("GetBread", new { id = breadId }, bread);
        }

        // DELETE: api/Breads/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBread(int id)
        //{
        //    var bread = await _context.Breads.FindAsync(id);
        //    if (bread == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Breads.Remove(bread);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool BreadExists(int id)
        //{
        //    return _context.Breads.Any(e => e.BreadId == id);
        //}
    }
}
