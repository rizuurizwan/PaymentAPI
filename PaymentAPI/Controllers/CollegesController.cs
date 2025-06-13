using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public CollegesController(PaymentDetailContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> GetColleges()
        {
            return await _context.Colleges.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<College>> GetCollege(int? id)
        {
            var college = await _context.Colleges.FindAsync(id);

            if (college == null)
            {
                return NotFound();
            }

            return college;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollege(int? id, College college)
        {
            if (id != college.age)
            {
                return BadRequest();
            }

            _context.Entry(college).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollegeExists(id))
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

        
        [HttpPost]
        public async Task<ActionResult<College>> PostCollege(College college)
        {
            _context.Colleges.Add(college);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollege", new { id = college.age }, college);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollege(int? id)
        {
            var college = await _context.Colleges.FindAsync(id);
            if (college == null)
            {
                return NotFound();
            }

            _context.Colleges.Remove(college);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollegeExists(int? id)
        {
            return _context.Colleges.Any(e => e.age == id);
        }

        [HttpGet("db")]
        public IActionResult TestConnection()
        {
            try
            {
                //var connection = _context.Database.GetDbConnection();
                //connection.Open();
                //return Ok("DB Connected Successfully");
                var connStr = _context.Database.GetDbConnection().ConnectionString;
                return Ok(connStr);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "DB Error: " + ex.Message);
            }
        }
    }
}
