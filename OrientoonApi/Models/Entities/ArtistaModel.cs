using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Models.Entities
{
    [Table("Artista")]
    public class ArtistaModel
    {
        [JsonIgnore]
        public string Id { get; set; }

        //[JsonProperty("NomeArtista")]
        [DataType(DataType.Text)]
        [Column("nome")]
        public string nome { get; set; }

        [JsonIgnore]
        public ICollection<OrientoonModel> Orientoons { get; set; }

        public ArtistaForm Converter()
        {
            return new ArtistaForm
            {
                Id = this.Id,
                Nome = this.nome
            };
        }
    }
}
