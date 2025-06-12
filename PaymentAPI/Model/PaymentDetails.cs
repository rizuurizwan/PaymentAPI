using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentAPI.Model
{
    public class PaymentDetails
    {
        [Key]
        public int PayemntDetailId { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string CarOwnerName { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string CardExpiryDate { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string SecurityCode { get; set; }
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public Employee Employee { get; set; }

    }
    public class PaymentDetailsDto
    {
        public int PaymentDetailId { get; set; }
        public string CarOwnerName { get; set; }
        public string CardNumber { get; set; }
    }
}
