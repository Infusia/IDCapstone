using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GameName { get; set; }

         public ICollection<PlayerVideo> PlayerVideos { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Video()
        {
            PlayerVideos = new HashSet<PlayerVideo>();
            Tags = new HashSet<Tag>();
          //  Comments = new HashSet<Comment>();

        }
    }
}
