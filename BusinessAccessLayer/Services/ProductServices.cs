using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModel;
using BulkyBook.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessAccessLayer.Interfaces;
using BulkyBook.DataAccess.Data;

namespace BulkyBook.Services
{
    public class ProductServices : IProduct
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext dbContext;

        public ProductServices(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts(bool includeCategory = false)
        {
            return _unitOfWork.Product.GetAll(includeProperties: includeCategory ? "Category" : null).ToList();
        }
        public IEnumerable<Product> GetProductsByCategoryId(int categoryid)
        {
            return dbContext.Products.Where(e=>e.CategoryId==categoryid).ToList();
        }

        public ProductVM GetProductViewModel(int? id)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == id);
            return new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = product
            };
        }

        public void UpsertProduct(ProductVM productVM, IFormFile? file)
        {

            //if (ModelState.IsValid)
            //{
                // Handle image upload logic (same as before)

                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();
            //}
        }

        public string DeleteProduct(int? id)
        {
            // Handle image deletion logic (same as before)
            var product = _unitOfWork.Product.Get(u => u.Id == id);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            return "Hello";
        }

    }
}
