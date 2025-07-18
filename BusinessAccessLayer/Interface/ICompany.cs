﻿using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Interface
{

    public interface ICompany
    {
        List<Company> GetAllCompanies();
        Company GetCompanyById(int id);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(int id);
    }

}
