using OrientoonApi.Models.Response;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models.Entities
{
    [Table("Status")]
    public class StatusModel
    {
        [Key]
        public string Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Status { get; set; }

        [JsonIgnore]
        public ICollection<OrientoonModel> Orientoons { get; set; }

        public StatusForm Converter()
        {
            return new StatusForm
            {
                Id = Id,
                Status = Status
            };
        }
    }
}
