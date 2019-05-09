using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataEF.Data;
using ODataEF.Data.Entities;

namespace ODataEF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ODataController
    {
        private DataContext _context;

        public CarController(DataContext context)
        {
            _context = context;
        }

        // GET api/cars
        [EnableQuery]
        public ActionResult Get()
        {
            if (_context.Cars.Count() == 0)
                AddSomeCars();

            var cars = _context.Cars.Include(b => b.Manufacturer);
            var sql = cars.ToSql(); 
            return Ok(cars);
        }

        

        // GET api/car(5)
        [EnableQuery]
        public ActionResult Get([FromODataUri]int key)
        {
            var result = _context.Cars.Include(b => b.Manufacturer).Where(b => b.Id == key);
            var sql = result.ToSql(); 
            return Ok(result.FirstOrDefault());
        }

        private void AddSomeCars()
        {
            List<Manufacturer> Manufacturers = new List<Manufacturer>();
            Manufacturers.Add(new Manufacturer { Name = "Ford" });
            Manufacturers.Add(new Manufacturer { Name = "Audi" });
            Manufacturers.Add(new Manufacturer { Name = "BMW" });
            Manufacturers.Add(new Manufacturer { Name = "Mercedes Benz" });

            List<Car> Cars = new List<Car>();
            Cars.Add(new Car() { Name = "Fusion", Manufacturer = Manufacturers.Where(f => f.Name == "Ford").FirstOrDefault() });
            Cars.Add(new Car() { Name = "Focus", Manufacturer = Manufacturers.Where(f => f.Name == "Ford").FirstOrDefault() });
            Cars.Add(new Car() { Name = "R8", Manufacturer = Manufacturers.Where(f => f.Name == "Audi").FirstOrDefault() });
            Cars.Add(new Car() { Name = "TT", Manufacturer = Manufacturers.Where(f => f.Name == "Audi").FirstOrDefault() });
            Cars.Add(new Car() { Name = "M3", Manufacturer = Manufacturers.Where(f => f.Name == "BMW").FirstOrDefault() });
            Cars.Add(new Car() { Name = "Z4", Manufacturer = Manufacturers.Where(f => f.Name == "BMW").FirstOrDefault() });
            Cars.Add(new Car() { Name = "SLS", Manufacturer = Manufacturers.Where(f => f.Name == "Mercedes Benz").FirstOrDefault() });
            Cars.Add(new Car() { Name = "AMG", Manufacturer = Manufacturers.Where(f => f.Name == "Mercedes Benz").FirstOrDefault() });

            foreach (var item in Manufacturers)
                _context.Manufacturers.Add(item);
            _context.SaveChanges();

            foreach (var item in Cars)
                _context.Cars.Add(item);
            _context.SaveChanges();

        }
    }
}
