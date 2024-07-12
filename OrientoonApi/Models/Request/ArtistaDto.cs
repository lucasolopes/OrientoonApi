using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class ArtistaDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]

        public string Nome { get; set; }

        
    }
}
