//using BulkyBook.DataAccess.Repository.IRepository;
//using BulkyBook.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace BulkyBookWeb.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    //[Authorize(Roles = SD.Role_Admin)];
//    public class CompanyController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public CompanyController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }
//        public IActionResult Index()
//        {
//            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

//            return View(objCompanyList);
//        }

//        public IActionResult Upsert(int? id)
//        {

//            if (id == null || id == 0)
//            {
//                //create
//                return View(new Company());
//            }
//            else
//            {
//                //update
//                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
//                return View(companyObj);
//            }

//        }
//        [HttpPost]
//        public IActionResult Upsert(Company CompanyObj)
//        {
//            if (ModelState.IsValid)
//            {

//                if (CompanyObj.Id == 0)
//                {
//                    _unitOfWork.Company.Add(CompanyObj);
//                }
//                else
//                {
//                    _unitOfWork.Company.Update(CompanyObj);
//                }

//                _unitOfWork.Save();
//                TempData["success"] = "Company created successfully";
//                return RedirectToAction("Index");
//            }
//            else
//            {

//                return View(CompanyObj);
//            }
//        }


//        #region API CALLS

//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
//            return Json(new { data = objCompanyList });
//        }


//        [HttpDelete]
//        public IActionResult Delete(int? id)
//        {
//            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
//            if (CompanyToBeDeleted == null)
//            {
//                return Json(new { success = false, message = "Error while deleting" });
//            }

//            _unitOfWork.Company.Remove(CompanyToBeDeleted);
//            _unitOfWork.Save();

//            return Json(new { success = true, message = "Delete Successful" });
//        }

//        #endregion
//    }
//}

using BulkyBook.Models;
using BusinessAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)];
    public class CompanyController : Controller
    {
        private readonly ICompany _companyService;

        public CompanyController(ICompany companyService)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _companyService.GetAllCompanies().ToList();
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _companyService.GetCompanyById(id.Value);
                return View(companyObj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    _companyService.AddCompany(companyObj);
                }
                else
                {
                    _companyService.UpdateCompany(companyObj);
                }

                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            bool deleted = _companyService.DeleteCompany(id.Value);

            if (!deleted)
            {
                TempData["error"] = "Error while deleting";
            }
            else
            {
                TempData["success"] = "Delete Successful";
            }

            return RedirectToAction("Index");
        }
    }
}
