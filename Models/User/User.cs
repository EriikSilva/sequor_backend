using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_backend.models
{
    public class User
    {
        [Key]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime InitialDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? EndDate { get; set; }

    }
}
