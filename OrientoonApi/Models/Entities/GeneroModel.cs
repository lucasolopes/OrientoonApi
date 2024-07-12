using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrientoonApi.Models.Response;
using OrientoonApi.Models.Request;

namespace OrientoonApi.Models.Entities
{
    [Table("Genero")]
    public class GeneroModel
    {

        public GeneroModel() { }

        public GeneroModel(GeneroDto genero)
        {
            this.nome = genero.Nome;
        }

        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

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
