using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly PaymentDetailContext learndata;
        public EmployeeController(PaymentDetailContext context)
        {
            this.learndata = context;
        }
        [HttpGet("getalldata")]
        public async Task<ActionResult> getallpaymentdtls()
        {
            string sqlquery = "exec getalldata";
            var data = await this.learndata.PaymentDetails.FromSqlRaw(sqlquery).ToListAsync();
            if (data == null)
            {
                return BadRequest("");
            }
            return Ok(data);
        }

    }
}
