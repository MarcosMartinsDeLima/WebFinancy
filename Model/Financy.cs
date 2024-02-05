using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFinancy.Model
{
    [Table("Financy")]
    public class Financy
    {   
        [Key]
        public int id {get;set;}
        [Column]
        [Required]
        [StringLength(300)]
        public string Nome {get;set;}

        [Column]
        [StringLength(300)]
        public string Descricao {get;set;}

        [Column]
        [Required]
        public float Valor {get;set;}

        [Column]
        [Required]
        public DateOnly Data {get;set;}
    }
}