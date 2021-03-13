using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceASP.Models
{
    public class Addresses
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
       
        public string City { get; set; }
        public string Country  { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        public int Phone { get; set; }
        public string PostalCode { get; set; }
      
    }
}
