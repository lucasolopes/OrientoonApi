using OrientoonApi.Enum;
using OrientoonApi.Models.Response;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models.Entities
{
    [Table("Status")]
    public class StatusModel
    {

        public StatusModel()
        {
        }

        public StatusModel(StatusEnum? status)
        {
            if (status != null)
                nome = status.GetValueOrDefault();
        }

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Column("nome",TypeName = "varchar(50)")]
        [MaxLength(50)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusEnum nome { get; set; }


        public ICollection<OrientoonModel> Orientoons { get; set; }

        public StatusForm Converter()
        {
            return new StatusForm
            {
                Id = Id,
                Status = nome
            };
        }
    }
}
