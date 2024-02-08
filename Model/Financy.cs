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
        public string Nome {get;set;} = string.Empty;

        [Column]
        [StringLength(300)]
        public string Descricao {get;set;} = string.Empty;

        [Column]
        [Required]
        public float Valor {get;set;}

        [Column]
        [Required]
        public DateOnly Data {get;set;}

        public User User{get;set;}
        public int IdUser {get;set;}
    }
}