using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class Production
    {
        [Column(TypeName = "bigint")]
        public long Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Order { get; set; }
        
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
