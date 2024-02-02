using BusinessAccessLayer.Interface;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Collections.Generic;

namespace BusinessAccessLayer.Services
{
    public class CompanyServices : ICompany
    {

        private readonly IUnitOfWork _unitOfWork;

        public CompanyServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Company> GetAllCompanies()
        {
            return _unitOfWork.Company.GetAll().ToList();
        }

        public Company GetCompanyById(int id)
        {
            return _unitOfWork.Company.Get(u => u.Id == id);
        }

        public void AddCompany(Company company)
        {
            _unitOfWork.Company.Add(company);
            _unitOfWork.Save();
        }

        public void UpdateCompany(Company company)
        {
            _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
        }

        public void DeleteCompany(int id)
        {
            var companyToDelete = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToDelete != null)
            {
                _unitOfWork.Company.Remove(companyToDelete);
                _unitOfWork.Save();
            }
        }

    }
}
