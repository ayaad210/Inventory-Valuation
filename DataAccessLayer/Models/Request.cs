using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Request
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }

        [MaxLength(100)]

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        [Required]
        public decimal count { get; set; }
        [Required]
        public decimal price { get; set; }
        public int date { get; set; }

    }
}
