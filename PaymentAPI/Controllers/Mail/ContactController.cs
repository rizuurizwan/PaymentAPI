using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace PaymentAPI.Controllers.Mail
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly EmailService _emailService;

        public ContactController()
        {
            _emailService = new EmailService();
        }

        [HttpPost("send-email")]
        public IActionResult SendEmail([FromBody] ContactFormModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _emailService.SendEmail(model);

            if (success)
                return Ok(new { message = "Email sent successfully" });

            return StatusCode(500, new { message = "Failed to send email" });
        }

        [Route("api/test")]
        [ApiController]
        public class TestController : ControllerBase
        {
            private readonly IConfiguration _configuration;

            public TestController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet("db")]
            public IActionResult TestConnection()
            {
                try
                {
                    var connStr = _configuration.GetConnectionString("DevConnection");
                    using (var conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        return Ok("DB Connected Successfully");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "DB Error: " + ex.Message);
                }
            }
        }

    }
}
