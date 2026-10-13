using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class Production
    {
        [Key]
        [Column(TypeName = "bigint(50)")]
        public string Id { get; set; }

        [Key]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Key]
        [Column(TypeName = "varchar(50)")]
        public string Order { get; set; }
        
        [Key]
        [Column(TypeName = "DateTime")]
        public DateTime Date { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MaterialCode { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal CycleTime { get; set; }
    }
}
