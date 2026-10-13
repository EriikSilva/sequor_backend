using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class ProductMaterial
    {
        [Key]
        [Column(TypeName = "varchar(50)")]
        public string ProductCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MaterialCode { get; set; }
    }
}
