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

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }


        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }

        public Rental() { }

        public Rental(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 5)
            {
                if (int.TryParse(parts[0], out int rentalId))
                    RentalId = rentalId;

                if (int.TryParse(parts[1], out int carId))
                    CarId = carId;

                if (int.TryParse(parts[2], out int customerId))
                    CustomerId = customerId;

                if (DateTime.TryParseExact(parts[3], "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime rentalStart))
                    RentalStart = rentalStart;

                if (DateTime.TryParseExact(parts[4], "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime rentalEnd))
                    RentalEnd = rentalEnd;
            }
        }
    }
}
