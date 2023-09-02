using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.Dtos.Item
{
    public class ItemReadDto
    {

      
        public int Id { get; set; }
       
        public string Name { get; set; }
        public decimal TotalCount { get; set; }



    }
}
