using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Repository
{
    public class RentalRepository : Repository<Rental>
    {
        public RentalRepository(CarRentalDbContext ctx) : base(ctx) { }

        public override Rental Read(int id)
        {
            return ctx.Rentals.FirstOrDefault(t => t.RentalId == id);
        }

        public override void Update(Rental rental)
        {
            var existingRental = Read(rental.RentalId);
            if (existingRental != null)
            {
                existingRental.CarId = rental.CarId;
                existingRental.CustomerId = rental.CustomerId;
                existingRental.RentalStart = rental.RentalStart;
                existingRental.RentalEnd = rental.RentalEnd;

                ctx.SaveChanges();
            }
        }
    }
}
