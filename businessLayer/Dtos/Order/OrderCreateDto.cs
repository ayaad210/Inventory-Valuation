using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.Dtos.Order
{
    public class OrderCreateDto
    {




        [Required(ErrorMessage = "Please select item.")]

        public int ItemId { get; set; }
        [Required(ErrorMessage = "Please enter Count.")]

        public decimal count { get; set; }
        [Required(ErrorMessage = "Please enter Price.")]
        public decimal price { get; set; }

    }
}
