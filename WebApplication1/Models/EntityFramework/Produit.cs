using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.EntityFramework
{
    [Table("t_e_produit_pdt")]
    public partial class Produit
    {
        [Key]
        [Column("pdt_id")]
        public int ProduitID { get; set; }

        [Required]
        [Column("pdt_nom")]
        [StringLength(100)]
        public string NomProduit { get; set; } = null!;

        [Column("pdt_description")]
        public string? Description { get; set; }

        [Column("pdt_nomPhoto")]
        public string? NomPhoto { get; set; }

        [Column("pdt_urlPhoto")]
        public string? UrlPhoto { get; set; }

        [Column("mrq_id")]
        public int MarqueID { get; set; }

        [ForeignKey(nameof(MarqueID))]
        [InverseProperty(nameof(Marque.ProduitMarque))]
        public virtual Marque MarqueProduit { get; set; } = null!;

        [Column("typ_id")]
        public int TypeProduitID { get; set; }

        [ForeignKey(nameof(TypeProduitID))]
        [InverseProperty(nameof(TypeProduit.Produit_TypeProduit))]
        public virtual TypeProduit TypeProduit_Produit { get; set; } = null!;

        [Column("pdt_stockreel")]
        public int StockReel { get; set; }

        [Column("pdt_stockmin")]
        public int StockMin { get; set; }

        [Column("pdt_stockmax")]
        public int StockMax { get; set; }
    }
}
