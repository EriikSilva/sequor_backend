using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class Material
    {
        [Key]
        [Column(TypeName = "varchar(50)")]
        public string MaterialCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MaterialDescription { get; set; }
    }
}
