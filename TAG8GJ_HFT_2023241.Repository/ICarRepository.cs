using ABC123_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC123_HFT_2023241.Repository
{
    public interface ICarRepository
    {
        void CreateCar(Car car);
        Car ReadCar(int id);
        IQueryable<Car> ReadAllCars();
        void UpdateCar(Car car);
        void DeleteCar(int id);

    }
}
