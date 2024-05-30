using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models
{
    [Table("Orientoon")]
    public class OrientoonModel
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        [Display(Name = "Titulo")]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(500)]
        public string CBanner { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public DateOnly DataLancamento { get; set; }


        public Double Avaliacao { get; set; }


        public ICollection<GeneroOrientoonModel> GeneroOrientoons { get; set; }


        public int ArtistaId { get; set; }

        [JsonIgnore]
        public ArtistaModel Artista { get; set; }


        public int AutorId { get; set; }

        [JsonIgnore]
        public AutorModel Autor { get; set; }


        public ICollection<TipoOrientoonModel> TipoOrientoon { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Status { get; set; }

        public ICollection<CapituloModel> Capitulos { get; set; }

    }
}
