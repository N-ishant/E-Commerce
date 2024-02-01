using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> GetAllProducts(bool includeCategory = false);
        ProductVM GetProductViewModel(int? id);
        void UpsertProduct(ProductVM productVM, IFormFile? file);
        string DeleteProduct(int? id);
        //IEnumerable<SelectListItem> GetCategoryList();
    }
}