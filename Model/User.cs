using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFinancy.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int IdUser {get;set;}

        [Column]
        [Required]
        [StringLength(200)]
        public string Nome {get;set;} = string.Empty;

        [Column]
        [Required]
        [StringLength(300)]
        public string Email {get;set;} = string.Empty;

        [Column]
        [Required]
        [StringLength(300)]
        public string Senha {get;set;} = string.Empty;

        public List<Financy>? financies {get;set;}

    }
}