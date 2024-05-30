using System.ComponentModel.DataAnnotations.Schema;

namespace OrientoonApi.Models
{
    [Table("TipoOrientoon")]
    public class TipoOrientoonModel
    {
        public string IdOrientoon { get; set; }
        public OrientoonModel Orientoon { get; set; }


        public int IdTipo { get; set; }
        public TipoModel Tipo { get; set; }
    }
}
