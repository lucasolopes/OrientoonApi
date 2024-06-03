using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models.Entities
{

        [Table("Autor")]
        public class AutorModel
        {
            [JsonIgnore]
            public int Id { get; set; }

            //  [JsonProperty("NomeAutor")]
            [DataType(DataType.Text)]
            public string NomeAutor { get; set; }

            [JsonIgnore]
            public ICollection<OrientoonModel> Orientoons { get; set; }
        }
}
