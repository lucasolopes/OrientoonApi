using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models
{
    [Table("Autor")]
    public class AutorModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string NomeAutor { get; set; }

        [JsonIgnore]
        public ICollection<OrientoonModel> Orientoons { get; set; }
    }
}
