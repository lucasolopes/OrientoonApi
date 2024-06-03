using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Entities
{
    [Table("Tipo")]
    public class TipoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string NomeTipo { get; set; }
        public ICollection<TipoOrientoonModel> TipoOrientoon { get; set; }
    }
}
