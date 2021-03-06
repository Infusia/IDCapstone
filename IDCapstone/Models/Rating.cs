using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public Video Video { get; set; }
        public ApplicationUser User { get; set; }
        [Range(0,5)]
        public int Score { get; set; }
        public int VideoId { get; set; }
    }
}
