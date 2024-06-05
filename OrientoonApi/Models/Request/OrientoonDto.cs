using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using OrientoonApi.Utils;
using Newtonsoft.Json;

namespace OrientoonApi.Models.Request
{
    public class OrientoonDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.Text)]
        [Newtonsoft.Json.JsonProperty("Titulo")]
       // [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.MultilineText, ErrorMessage = "O campo {0} Esta com o formato invalido!")]
        public string Descricao { get; set; }


        
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} Esta com o formato invalido!")]
        [JsonProperty("DataLancamento")]
        //regex para validar a data no formato dd/MM/yyyy HH:mm:ss
        //[RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4} ([0-1][0-9]|(2)[0-3]):([0-5][0-9]):([0-5][0-9])$", ErrorMessage = "Data invalida!")]

        [JsonConverter(typeof(StrictDateTimeConverter))]
        public DateTime? DataLancamento { get; set; }


        //public ICollection<String> GeneroOrientoons { get; set; }
        //
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.Text, ErrorMessage = "O campo {0} Esta com o formato invalido!")]
        public string NomeArtista { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.Text, ErrorMessage = "O campo {0} Esta com o formato invalido!")]
        public string NomeAutor { get; set; }


        // public ICollection<String> TipoOrientoons { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [DataType(DataType.Text, ErrorMessage = "O campo {0} Esta com o formato invalido!")]
        public string Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        public bool AdultContent { get; set; } 


        public OrientoonModel Converter()
        {
            return new OrientoonModel(Titulo, Descricao, DataLancamento, AdultContent);
        }

    }
}
