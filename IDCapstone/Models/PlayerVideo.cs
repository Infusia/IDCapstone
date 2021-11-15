using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class PlayerVideo
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int VideoId { get; set; }
        public Player Player { get; set; }
        public Video Video { get; set; }
    }
}
