
using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Response
{
    public class OrientoonForm
    {
        public string Id { get; set; }
        [Newtonsoft.Json.JsonProperty("Titulo")]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DataLancamento { get; set; }
        public string NomeArtista { get; set; }
        public string NomeAutor { get; set; }
        public string Status { get; set; }
        public bool AdultContent { get; set; }

        public OrientoonModel Converter() {             
            return new OrientoonModel
            {
                Id = this.Id,
                Titulo = this.Titulo,
                Descricao = this.Descricao,
                DataLancamento = this.DataLancamento,
                AdultContent = this.AdultContent
            };
        }
    }
}
