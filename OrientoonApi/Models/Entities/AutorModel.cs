using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Models.Entities
{

        [Table("Autor")]
        public class AutorModel
        {
            [JsonIgnore]
            public string Id { get; set; }

            //  [JsonProperty("NomeAutor")]
            [DataType(DataType.Text)]
            public string NomeAutor { get; set; }

            [JsonIgnore]
            public ICollection<OrientoonModel> Orientoons { get; set; }

            public AutorForm Converter()
        {
                return new AutorForm
                {
                    Id = Id,
                    Nome = NomeAutor
                };
            }

        }
}
