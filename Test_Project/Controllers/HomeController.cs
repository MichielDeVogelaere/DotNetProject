using Microsoft.AspNetCore.Mvc;
using Test_Project.Models;

namespace Test_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new PersonModel());
        }

        [HttpPost]
        public IActionResult Index(PersonModel person)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            
            return View(person);
        }

        
       [HttpGet]
        public IActionResult IsBelgiumResident(string country)
        {
            bool isBelgianResident = string.Equals(country, "Belgium", StringComparison.OrdinalIgnoreCase);
    
            return Json(isBelgianResident);
        }


        public IActionResult Success()
        {
            return Success(); // Placeholder
        }
    }
}
