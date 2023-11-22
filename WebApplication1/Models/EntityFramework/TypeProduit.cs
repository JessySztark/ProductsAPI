using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.EntityFramework
{
    [Table("t_e_typeproduit_typ")]
    public class TypeProduit
    {
        [Key]
        [Column("typ_id")]
        public int TypeProduitID { get; set; }

        [Required]
        [Column("typ_nom")]
        [StringLength(100)]
        public string NomTypeProduit { get; set; } = null!;

        [InverseProperty(nameof(Produit.TypeProduit_Produit))]
        public virtual ICollection<Produit>? Produit_TypeProduit { get; set; }
    }
}
