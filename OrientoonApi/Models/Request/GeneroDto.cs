using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Request
{
    public class GeneroDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]

        public string Nome { get; set; }

        public GeneroModel Converter()
        {
            return new GeneroModel
            {
                NomeGenero = Nome
            };
        }
    }
}
