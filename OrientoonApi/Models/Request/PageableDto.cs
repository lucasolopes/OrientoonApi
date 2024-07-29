using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class PageableDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        public int batchSize { get; set; } 
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public int pageNumber { get; set; }
    }
}
