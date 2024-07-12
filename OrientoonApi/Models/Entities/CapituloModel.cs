using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrientoonApi.Models.Response;
using OrientoonApi.Models.Request;

namespace OrientoonApi.Models.Entities
{

        [Table("Capitulo")]
        public class CapituloModel
        {
            public CapituloModel() { }

            public CapituloModel(CapituloDto capituloDto, string orientoonId,string caminho)
            {
                NumCapitulo = capituloDto.numCap;
                DataLancamento = capituloDto.dataLancamento;
                OrientoonId = orientoonId;
                Caminho = caminho;
            }

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
            public DateTime? DataLancamento { get; set; }

            public Double AvaliacaoCap { get; set; }

            public string OrientoonId { get; set; }

            public OrientoonModel Orientoon { get; set; }

            public List<ImagemModel> Imagens { get; set; }

            public CapituloInfoForm Converter() 
            {
            return new CapituloInfoForm
            {
                Id = Id,
                NumCapitulo = NumCapitulo,
                DataLancamento = DataLancamento,
                AvaliacaoCap = AvaliacaoCap,
                OrientoonId = OrientoonId,
            };
            }
        }
}
