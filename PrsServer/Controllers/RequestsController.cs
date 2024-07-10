using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using PrsServer.Data;
using PrsServer.Models;

namespace PrsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase {

        private const string Approved = "APPROVED";
        private const string Review = "REVIEW";
        private const string Reject = "REJECTED";

        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/Requests/Status
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestByStatus(String status)
        {
            return await _context.Requests
                                        .Include(x => x.User)
                                        .Include(x => x.Status == status).ToListAsync();
        }

        // GET: api/Requests/Approve
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            if(_context.Requests == null)
            {
                return NotFound();
            }
            return await _context.Requests.Include(x => x.User).ToListAsync();
        }

        //GET api/Requests/Reviews/UserId
        [HttpGet("review/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> ReviewRequest(int id){

            return await _context.Requests.Include(x => x.User).Where(x => x.Status == Review && x.UserId != id).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //PUT: /apiRequests/review/
        [HttpPut("review/{id}")]
        public async Task<IActionResult> ReviewRequest(int id, Request request)
        {
            if (request == null)
            {
                return NotFound();
            }
            request.Status = (request.Total <= 50) ?  Approved : Review;
            return await PutRequest(id, request);
        }

        //PUT: /api/requests/approve
        [HttpPut("approve /{id}")]
        public async Task<IActionResult> ApproveRequest(int id, Request request)
        {
            if (request == null)
            {
                return NotFound();
            }
            if (request.Status == Approved)
            {
                return Ok(request.Status);
            }
            request.Status = Approved;
            await _context.SaveChangesAsync();
            return await PutRequest(id, request);
        }
        //PUT: api/Request/Reject
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectRequest(int id, Request request)
        {
            if (request == null)
            {
                return NotFound();
            }
            if (request.Status == Reject)
            {
                return Ok(request.Status);
            }
            request.Status = Reject;
            await _context.SaveChangesAsync();
            return await PutRequest(id, request);
        }

            // PUT: api/Requests/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
