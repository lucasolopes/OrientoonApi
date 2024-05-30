using System.ComponentModel.DataAnnotations.Schema;

namespace OrientoonApi.Models
{
    [Table("GeneroOrientoon")]
    public class GeneroOrientoonModel
    {
        public string IdOrientoon { get; set; }
        public OrientoonModel Orientoon { get; set; }

        public int IdGenero { get; set; }
        public GeneroModel Genero { get; set; }
    }
}
