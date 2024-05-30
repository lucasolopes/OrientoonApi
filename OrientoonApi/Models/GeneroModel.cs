using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models
{
    [Table("Genero")]
    public class GeneroModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string NomeGenero { get; set; }
        public ICollection<GeneroOrientoonModel> GeneroOrientoons { get; set; }
    }
}
