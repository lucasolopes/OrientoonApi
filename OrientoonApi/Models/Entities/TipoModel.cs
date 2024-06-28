using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Models.Entities
{
    [Table("Tipo")]
    public class TipoModel
    {
        [Key]
        [Required]
        public string Id { get; set; }


        [Column("nome")]
        public string NomeTipo { get; set; }
        public ICollection<TipoOrientoonModel> TipoOrientoon { get; set; }

        public TipoForm Converter()
        {
            return new TipoForm
            {
                Id = this.Id,
                Nome = this.NomeTipo
            };
        }
    }
}
