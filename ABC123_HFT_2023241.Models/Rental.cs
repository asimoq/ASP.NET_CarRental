using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC123_HFT_2023241.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalId { get; set; }

        
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }
    }
}
