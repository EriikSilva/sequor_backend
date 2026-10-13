using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class Product
    {
        [Key]
        [Column(TypeName = "varchar(50)")]
        public string ProductCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ProductDescription { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Image { get; set; }
     
        [Column(TypeName = "numeric(18,2)")]
        public decimal CycleTime { get; set; }
    }
}
