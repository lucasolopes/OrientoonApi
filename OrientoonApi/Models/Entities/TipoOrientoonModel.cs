using System.ComponentModel.DataAnnotations.Schema;

namespace OrientoonApi.Models.Entities
{
    [Table("TipoOrientoon")]
    public class TipoOrientoonModel
    {
        public string IdOrientoon { get; set; }
        public OrientoonModel Orientoon { get; set; }


        public string IdTipo { get; set; }
        public TipoModel Tipo { get; set; }
    }
}
