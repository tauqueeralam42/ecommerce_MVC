using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        // Dependency injection for ApplicationDbContext
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Action method to display the list of categories
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // Action method to show the create category form
        public IActionResult Create()
        {
            return View();
        }

        // Action method to handle the form submission for creating a new category
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Custom validation to ensure Name and DisplayOrder are not the same
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can not be same");
            }

            // Check if the model state is valid before saving to the database
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }

            // If model state is not valid, return the same view with validation errors
            return View();
        }
    }
}
