using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class FlaggedComment
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Comment Comment { get; set; }

    }
}
