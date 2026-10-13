using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class ProductMaterial
    {
        [Column(TypeName = "varchar(50)")]
        public string ProductCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MaterialCode { get; set; }

        public int SortOrder { get; set; }
    }
}
