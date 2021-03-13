using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceASP.Models
{
    public class Comments
    {
        public int Id { get; set; }
        [ForeignKey("Items")]
        public int ItemId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
       

        public string CommentBody { get; set; }
        public string CommentTitle { get; set; }
       
        public decimal Rating { get; set; }
    }
}
