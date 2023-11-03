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
                TempData["success"] = "The villa is created successfully.";

                return RedirectToAction("Index", "Villa");
            }

            return View();
        }

        public IActionResult Update(int? villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            if (obj == null) 
            {
                return RedirectToAction("Error", "Home");
            }
            //_db.Villas.Update(obj);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Update (Villa villa)
        {            
            if (ModelState.IsValid)
            {
                _db.Villas.Update(villa);
                _db.SaveChanges();
                TempData["success"] = "The villa has been updated successfully.";

                return RedirectToAction("Index", "Villa");
            }

            return View();
        }

        public IActionResult Delete(int? villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            //_db.Villas.Update(obj);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == villa.Id);

            if (objFromDb != null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully.";

                return RedirectToAction("Index", "Villa");
            }

            TempData["error"] = "The villa couldn't delete.";
            return View();
        }
    }
}
