using Azure.Core;
using Microsoft.EntityFrameworkCore;
using OptiLogic_API.HelperModel;
using OptiLogic_API.Model;

namespace OptiLogic_API.Utility
{
    public class CarFilter
    {
        //Returnerer en liste af biler som skal tilføjes til databasen.
        public List<Car> CarsToAdd(List<Car> newPrediction, List<Car> oldPrediction)
        {
            var carsToAdd = new List<Car>();


            foreach (var car in newPrediction)
            {
                bool carExists = oldPrediction.Any(p =>
                    Similar(car.XAxis, p.XAxis) &&
                    Similar(car.YAxis, p.YAxis) &&
                    Similar(car.Width, p.Width) &&
                    Similar(car.Height, p.Height));

                if (!carExists)
                {
                    carsToAdd.Add(car);
                }
            }

            return carsToAdd;

        }

        //Returnerer en liste af biler som skal fjernes fra databasen, da de ikke længere er i framen.
        public List<Car> CarsToRemove(List<Car> oldPrediction, List<Car> newPrediction)
        {
            var carsToRemove = new List<Car>();

            foreach (var car in oldPrediction)
            {
                bool carIsMissing = !newPrediction.Any(p =>
                Similar(car.XAxis, p.XAxis) &&
                Similar(car.YAxis, p.YAxis) &&
                Similar(car.Width, p.Width) &&
                Similar(car.Height, p.Height));

                if (carIsMissing)
                {
                    carsToRemove.Add(car);
                }

            }


            return carsToRemove;
        }

        //Matematik metode som returnerer sandt/falsk, om 2 værdier er similar
        public static bool Similar(float a, float b)
        {
            return Math.Abs(a - b) <= 10;
        }
    }



}
