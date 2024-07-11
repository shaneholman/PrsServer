using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsServer.Data;
using PrsServer.Models;

namespace PrsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendorsController(AppDbContext context)
        {
            _context = context;
        }



        //CREATING THE PO METHOD
       /* [HttpGet("po/{VendorId}")]
        public async Task<ActionResult<PO>> CreatePo(int VendorId) { 
        

            var Vendor = await _context.Vendors.FindAsync(VendorId);

            //if (Vendor == null) {

                //return NotFound();
            

            var values = await (from p in _context.RequestLines
                                where p.Request.Status == "APPROVED"
                                && p.Product.VendorId == VendorId
                                group p by p.ProductId into a select new
                                {
                                    Product = a.Key,
                                    TotalSum = a.Sum(p => p.Quantity * p.Product.Price)
                                }).ToListAsync();

            List<POline> POlines = new List<POline>();

            foreach (var p in values)
            {
                int i = 0;
                POlines.Add(new POline { Product = p.Product, Quantity = p.TotalSum, LineTotal = p.LineTotal });
                i++;
            }
            var Pototal = (from p in POlines select p.LineTotal).Sum();
            var po = new po(Vendor, POlines, Pototal);

            return(po);

        }*/



        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendor()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
