using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public Video Video { get; set; }
        public int videoId { get; set; }
    }
}
