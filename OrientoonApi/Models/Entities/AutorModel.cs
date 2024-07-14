using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrientoonApi.Models.Response;
using OrientoonApi.Models.Request;

namespace OrientoonApi.Models.Entities
{

        [Table("Autor")]
        public class AutorModel
        {
            public AutorModel() {}
        
            public AutorModel(AutorDto autorDto)
            {
                nome = autorDto.Nome;
            }

            [Key]
            [JsonIgnore]
            public string Id { get; set; } = Guid.NewGuid().ToString("N");

        //  [JsonProperty("NomeAutor")]
        [DataType(DataType.Text)]
            [Column("nome")]
            public string nome { get; set; }


            public ICollection<OrientoonModel> Orientoons { get; set; }

            public AutorForm Converter()
        {
                return new AutorForm
                {
                    Id = Id,
                    Nome = nome
                };
            }

        }
}
