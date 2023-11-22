using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.EntityFramework
{
    [Table("t_e_marque_mrq")]
    public class Marque
    {
        [Key]
        [Column("mrq_id")]
        public int MarqueID { get; set; }

        [Required]
        [Column("mrq_name")]
        [StringLength(100)]
        public String NomMarque { get; set; }

        [InverseProperty(nameof(Produit.MarqueProduit))]
        public virtual ICollection<Produit>? ProduitMarque { get; set; }
    }
}
