using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Interface
{
    public interface ICompany
    {
        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyById(int companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        bool DeleteCompany(int companyId);
    }
}
}
