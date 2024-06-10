using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class AutorDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]

        public string Nome { get; set; }

        public AutorModel Converter()
        {
            return new AutorModel
            {
                NomeAutor = this.Nome
            };
        }
    }
}
