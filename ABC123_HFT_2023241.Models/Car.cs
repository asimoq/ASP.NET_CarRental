using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC123_HFT_2023241.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(20)]
        public string LicencePlate { get; set; }

        [Range(1950,2500)]
        public int Year { get; set; }

        [Range(0,300000)]
        public int DailyRentalCost { get; set; }

        

        public Car() { }

        public Car(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 6)
            {
                if (int.TryParse(parts[0], out int carID))
                    CarID = carID;

                Brand = parts[1];
                Model = parts[2];
                LicencePlate = parts[3];

                if (int.TryParse(parts[4], out int year))
                    Year = year;

                if (int.TryParse(parts[5], out int cost))
                    DailyRentalCost = cost;
            }
        }
    }
}
