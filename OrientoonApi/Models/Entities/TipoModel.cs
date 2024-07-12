using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrientoonApi.Models.Response;
using OrientoonApi.Models.Request;

namespace OrientoonApi.Models.Entities
{
    [Table("Tipo")]
    public class TipoModel
    {
        public TipoModel() { }

        public TipoModel(TipoDto tipo)
        {
            this.nome = tipo.Nome;
        }

        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");


        [Column("nome")]
        public string nome { get; set; }
        public ICollection<TipoOrientoonModel> TipoOrientoon { get; set; }

        public TipoForm Converter()
        {
            return new TipoForm
            {
                Id = this.Id,
                Nome = this.nome
            };
        }
    }
}
