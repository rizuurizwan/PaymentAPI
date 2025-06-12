using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Model
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

    }
}
