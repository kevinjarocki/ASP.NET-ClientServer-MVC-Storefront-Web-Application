using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CaseStudy.Models
{
    public class Product
    {
        [Key]
        [StringLength(8)]
        public string Id { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; } // generates FK
        public int BrandId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(20)]
        public string GraphicName { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal MSRP { get; set; }
        [Required]
        public int QtyOnHand { get; set; }
        [Required]
        public int QtyOnBackOrder { get; set; }
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
       
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }
    }
}