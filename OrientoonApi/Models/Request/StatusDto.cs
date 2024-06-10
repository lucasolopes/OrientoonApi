using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class StatusDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]

        public string Status { get; set; }
        public StatusModel Converter()
        {
            return new StatusModel
            {
                Status = this.Status
            };
        }
    }
}
