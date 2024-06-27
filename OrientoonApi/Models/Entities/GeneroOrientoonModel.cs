using System.ComponentModel.DataAnnotations.Schema;

namespace OrientoonApi.Models.Entities
{
    [Table("GeneroOrientoon")]
    public class GeneroOrientoonModel
    {
        public string IdOrientoon { get; set; }
        public OrientoonModel Orientoon { get; set; }

        public string IdGenero { get; set; }
        public GeneroModel Genero { get; set; }
    }
}
