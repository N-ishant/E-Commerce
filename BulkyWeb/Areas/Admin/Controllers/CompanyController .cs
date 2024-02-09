using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using BusinessAccessLayer.Interface;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly ICompany CompanyService;

        public CompanyController(ICompany companyService)
        {
            CompanyService = companyService;
        }

        public IActionResult Index()
        {
            return View(CompanyService.GetAllCompanies());
        }
        public IActionResult Upsert(int? id)
        {
            Company company = null;
            if (id == null || id == 0)
            {
                // create
                company = new Company();
            }
            else
            {
                // update
                company = CompanyService.GetCompanyById(id.Value);
            }

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    CompanyService.AddCompany(company);
                }
                else
                {
                    CompanyService.UpdateCompany(company);
                }

                return RedirectToAction("Index");
            }

            return View(company);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = CompanyService.GetAllCompanies();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToDelete = CompanyService.GetCompanyById(id.Value);
            if (companyToDelete != null)
            {
                CompanyService.DeleteCompany(id.Value);
                return Json(new { success = true, message = "Delete Successful" });
            }
            else
            {
                return NotFound();
            }
        }

        #endregion
    }
}