using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public ValuesController(PaymentDetailContext context)
        {
            _context = context;
        }
        [HttpGet("Getallsp")]
        public async Task<IActionResult> Getallsp()
        {
            string queries = "exec getalldata";
            var data = await this._context.PaymentDetails.FromSqlRaw(queries).ToListAsync();
            return Ok(data);
        }

        [HttpPost("getbyid")]
        public async Task<IActionResult> GetById(employeeinsert aemployee)
        {
            string queries = "exec employeeinsert @first_name,@last_name,@email";
            SqlParameter[] parameters =
            {
                new SqlParameter("@first_name",aemployee.first_name),
                new SqlParameter("@last_name",aemployee.last_name),
                new SqlParameter("@email",aemployee.email)
            };
            var data = await this._context.Database.ExecuteSqlRawAsync(queries, parameters);
            return Ok(data);
        }

        [HttpPost("crudopertion")]
        public async Task<IActionResult> CrudOperationi(employeeinsert aemployee)
        {
            string queries = "exec crudopertion @mode,@employee_id,@first_name,@last_name,@email";
            SqlParameter[] parameter = 
            {
                new SqlParameter("@mode",aemployee.mode),
                new SqlParameter("@employee_id", aemployee.employee_id),
                new SqlParameter("@first_name", aemployee.first_name),
                new SqlParameter("@last_name", aemployee.last_name),
                new SqlParameter("@email", aemployee.email),
            };
            if(aemployee.mode == 1)
            {
                var result = await this._context.Database.ExecuteSqlRawAsync(queries, parameter);
                return Ok("Data Inserted Successfully");
            }
            else if (aemployee.mode == 2)
            {
                var result = await this._context.Database.ExecuteSqlRawAsync(queries, parameter);
                return Ok("Data Updated Successfully");
            }
            else if (aemployee.mode == 3)
            {
                var result = await _context.Employees
                    .FromSqlRaw(queries, parameter)
                    .ToListAsync();

                return Ok(result);
            }
            else if(aemployee.mode == 4)
            {
                var result =  _context.Employees.FromSqlRaw(queries, parameter).AsNoTracking().AsEnumerable().FirstOrDefault();
                return Ok(result);
            }
            else
            {
                var affectedRows = await _context.Database.ExecuteSqlRawAsync(queries, parameter);
                return Ok("Deleted Successfully");
            }
        }
    }
}
