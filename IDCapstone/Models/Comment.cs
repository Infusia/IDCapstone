
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        [DataType(DataType.Time)]
        public DateTime TimeStamp { get; set; }
        public ApplicationUser User { get; set; }
        public Video Video { get; set; }
    }
}
