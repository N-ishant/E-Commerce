//using BusinessAccessLayer.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessAccessLayer.Services
//{
//    public class CompanyServices :ICompany
//    {
//    }
//}

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

        public void AddCompany(Company company)
        {
            _unitOfWork.Company.Add(company);
            _unitOfWork.Save();
        }

        public bool DeleteCompany(int companyId)
        {
            var company = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == companyId);
            if (company != null)
            {
                _unitOfWork.Company.Remove(company);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _unitOfWork.Company.GetAll();
        }

        public Company GetCompanyById(int companyId)
        {
            return _unitOfWork.Company.GetFirstOrDefault(c => c.Id == companyId);
        }

        public void UpdateCompany(Company company)
        {
            _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
        }
    }
}

