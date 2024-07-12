using OrientoonApi.Enum;
using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class StatusDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]

        public StatusEnum? Status { get; set; }
        
    }
}
