using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Model
{
    public class College
    {
        [Key]
        public int age { get; set; }
        public string value {  get; set; }
        public string description { get; set; }
        
    }
}
