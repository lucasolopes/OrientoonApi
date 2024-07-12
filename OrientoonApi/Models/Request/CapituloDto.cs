using Newtonsoft.Json;
using OrientoonApi.Utils;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class CapituloDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        public double numCap { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} Esta com o formato invalido!")]
        [JsonConverter(typeof(StrictDateTimeConverter))]
        public DateTime? dataLancamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        public IList<IFormFile> files { get; set;}
    }
}
