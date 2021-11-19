using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public ApplicationUser User { get; set; }
        public Video Video { get; set; }

        public string UserId { get; set; }
    }
}
