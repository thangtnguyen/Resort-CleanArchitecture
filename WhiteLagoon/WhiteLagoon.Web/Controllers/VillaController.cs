using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastucture.Data;

namespace WhiteLagoon.Web.Controllers
{

    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();

            return View(villas);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa) 
        {
            if (villa.Description == villa.Name)
            {
                ModelState.AddModelError("Name", "The Description cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Villas.Add(villa);
                _db.SaveChanges();

                return RedirectToAction("Index", "Villa");
            }

            return View();
        }
    }
}
