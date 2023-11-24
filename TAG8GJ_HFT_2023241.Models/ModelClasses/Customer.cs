using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TAG8GJ_HFT_2023241.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [StringLength(30)]
        public string CustomerPhone { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rental> Rentals { get; set; }


        public Customer() { }

        public Customer(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 4)
            {
                if (int.TryParse(parts[0], out int customerId))
                    CustomerId = customerId;

                CustomerName = parts[1];
                CustomerEmail = parts[2];
                CustomerPhone = parts[3];
            }
        }
    }
}
