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
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }

            // If model state is not valid, return the same view with validation errors
            return View();
        }

  
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
         
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can not be same");
            }

            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Category? categoryFromDb = _db.Categories.Find(id);
        //    if (categoryFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoryFromDb);
        //}

        [HttpPost]
        public IActionResult Delete(int? id)
        {

          Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
