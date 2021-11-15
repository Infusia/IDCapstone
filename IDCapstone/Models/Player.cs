using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string Bio { get; set; }
        public string TwitterUrl { get; set; }

        public ICollection<PlayerVideo> PlayerVideos { get; set; }
      

        public Player()
        {
         
            PlayerVideos = new HashSet<PlayerVideo>();

        }
    }
}
