using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Entities
{

        [Table("Capitulo")]
        public class CapituloModel
        {
            [Key]
            [Required]
            public string Id { get; set; } = Guid.NewGuid().ToString();

            [Required]
            public Double NumCapitulo { get; set; }

            [Required]
            [MaxLength(255)]
            [MinLength(1)]
            [DataType(DataType.Text)]
            [StringLength(255)]
            public string Caminho { get; set; }

            [Required]
            public DateOnly DataLancamento { get; set; }

            public Double AvaliacaoCap { get; set; }

            public string OrientoonId { get; set; }

            public OrientoonModel Orientoon { get; set; }

            public List<ImagemModel> Imagens { get; set; }
        }
}
