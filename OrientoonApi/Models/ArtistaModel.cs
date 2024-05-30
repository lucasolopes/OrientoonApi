using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models
{
    [Table("Artista")]
    public class ArtistaModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string NomeArtista { get; set; }

        [JsonIgnore]
        public ICollection<OrientoonModel> Orientoons { get; set; }
    }
}
