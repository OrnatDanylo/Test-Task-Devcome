using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? OrderCost { get; set; }
        public string? ItemsDecription { get; set; }
        public string? ShipingAddress { get; set; }



        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}
