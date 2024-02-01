using BulkyBook.Models;
using BusinessAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers

{

    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller

    {


        private readonly ICategory CategoryService;

        //public CategoryController(IUnitOfWork unitOfWork)

        //{

        //    _unitOfWork = unitOfWork;

        //}

        public CategoryController(ICategory categoryService)

        {

            CategoryService = categoryService;

        }

        public IActionResult Index()

        {

            List<Category> objCategoryList = CategoryService.GetAllCategories();

            return View(objCategoryList);

        }

        public IActionResult Create()

        {

            return View();

        }

        //[HttpPost]

        //public IActionResult Create(Category obj)

        //{

        //    if (obj.Name == obj.DisplayOrder.ToString())

        //    {

        //        ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

        //    }

        //    if (ModelState.IsValid)

        //    {

        //        _unitOfWork.Category.Add(obj);

        //        _unitOfWork.Save();

        //        TempData["success"] = "Category created successfully";

        //        return RedirectToAction("Index");

        //    }

        //    return View();

        //}

        [HttpPost]

        public IActionResult Create(Category obj)

        {

            if (obj.Name == obj.DisplayOrder.ToString())

            {

                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

            }

            if (ModelState.IsValid)

            {

                CategoryService.AddCategory(obj);

                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");

            }

            return View();

        }


        //public IActionResult Edit(int? id)

        //{

        //    if (id == null || id == 0)

        //    {

        //        return NotFound();

        //    }

        //    Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

        //    //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);

        //    //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

        //    if (categoryFromDb == null)

        //    {

        //        return NotFound();

        //    }

        //    return View(categoryFromDb);

        //}

        public IActionResult Edit(int? id)

        {

            if (id == null || id == 0)

            {

                return NotFound();

            }

            Category category = CategoryService.GetCategoryById(id.Value);

            if (category == null)

            {

                return NotFound();

            }

            return View(category);

        }


        //[HttpPost]

        //public IActionResult Edit(Category obj)

        //{

        //    if (ModelState.IsValid)

        //    {

        //        _unitOfWork.Category.Update(obj);

        //        _unitOfWork.Save();

        //        TempData["success"] = "Category updated successfully";

        //        return RedirectToAction("Index");

        //    }

        //    return View();

        //}

        [HttpPost]

        public IActionResult Edit(Category obj)

        {

            if (ModelState.IsValid)

            {

                CategoryService.UpdateCategory(obj);

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");

            }

            return View(obj);

        }

        //public IActionResult Delete(int? id)

        //{

        //    if (id == null || id == 0)

        //    {

        //        return NotFound();

        //    }

        //    Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

        //    if (categoryFromDb == null)

        //    {

        //        return NotFound();

        //    }

        //    return View(categoryFromDb);

        //}

        public IActionResult Delete(int? id)

        {

            if (id == null || id == 0)

            {

                return NotFound();

            }

            Category category = CategoryService.GetCategoryById(id.Value);

            if (category == null)

            {

                return NotFound();

            }

            return View(category);

        }

        //[HttpPost, ActionName("Delete")]

        //public IActionResult DeletePOST(int? id)

        //{

        //    Category? obj = _unitOfWork.Category.Get(u => u.Id == id);

        //    if (obj == null)

        //    {

        //        return NotFound();

        //    }

        //    _unitOfWork.Category.Remove(obj);

        //    _unitOfWork.Save();

        //    TempData["success"] = "Category deleted successfully";

        //    return RedirectToAction("Index");

        //}

        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? id)

        {

            if (id == null || id == 0)

            {

                return NotFound();

            }

            CategoryService.DeleteCategory(id.Value);

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");

        }

    }

}
