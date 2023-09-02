using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public decimal TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Request>? Requests { get; set; }
        public List<Order>? Orders { get; set; }

    }


}