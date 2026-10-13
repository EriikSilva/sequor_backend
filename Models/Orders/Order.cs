using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class OrderProduction
    {
        [Key]
        [Column(TypeName = "varchar(50)")]
        public string Order { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ProductCode { get; set; }
        
    }
}
