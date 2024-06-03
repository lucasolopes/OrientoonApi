using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models.Entities
{
    [Table("Artista")]
    public class ArtistaModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        //[JsonProperty("NomeArtista")]
        [DataType(DataType.Text)]
        public string NomeArtista { get; set; }

        [JsonIgnore]
        public ICollection<OrientoonModel> Orientoons { get; set; }
    }
}
