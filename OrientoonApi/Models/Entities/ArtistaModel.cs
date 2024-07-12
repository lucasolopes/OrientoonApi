using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrientoonApi.Models.Response;
using OrientoonApi.Models.Request;

namespace OrientoonApi.Models.Entities
{
    [Table("Artista")]
    public class ArtistaModel
    {
        public ArtistaModel ()
        {
        }

        public ArtistaModel (ArtistaDto artistaDto)
        {
            this.nome = artistaDto.Nome;
        }

        [Key]
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

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
