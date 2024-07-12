
using OrientoonApi.Enum;
using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Response
{
    public class OrientoonForm
    {
        public string Id { get; set; }
        [Newtonsoft.Json.JsonProperty("Titulo")]
        public string Titulo { get; set; }
        public string Banner { get; set; }
        public string Descricao { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DataLancamento { get; set; }
        public double Avaliacao { get; set; }
        public string NomeArtista { get; set; }
        public string NomeAutor { get; set; }
        public StatusEnum Status { get; set; }
        public bool AdultContent { get; set; }
        public List<GeneroForm> Generos { get; set; }
        public List<TipoForm> Tipos { get; set; }

        public OrientoonModel Converter() {             
            return new OrientoonModel
            {
                Id = this.Id,
                nome = this.Titulo,
                Descricao = this.Descricao,
                DataLancamento = this.DataLancamento,
                AdultContent = this.AdultContent
            };
        }
    }
}
