using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Model
{
    public class Employee
    {
        [Key]
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        //navigation
        public ICollection<PaymentDetails> PaymentDetails { get; set; }
    }

    public class employeeinsert
    {
        public int mode { get; set; }
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
    }
}
