using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceASP.Models
{
    public class Orderitems
    {
        public int Id { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        
        public int Quantity { get; set; }
     
    }
}
