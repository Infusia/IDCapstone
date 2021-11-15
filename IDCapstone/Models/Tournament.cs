using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public string TournamentOrganizer { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        public decimal PrizePool { get; set; }
        public string Games { get; set; }
        public string Location { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime TournamentDate { get; set; }
        public string MapsLocation { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
    }
}
