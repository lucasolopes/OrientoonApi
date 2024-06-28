using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Models.Entities
{
    [Table("Genero")]
    public class GeneroModel
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Column("nome")]
        public string nome { get; set; }
        public ICollection<GeneroOrientoonModel> GeneroOrientoons { get; set; }

        public GeneroForm Converter()
        {
            return new GeneroForm
            {
                Id = this.Id,
                Nome = this.nome
            };
        }
    }
}
