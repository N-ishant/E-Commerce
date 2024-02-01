using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BusinessAccessLayer.Interfaces;

 
namespace BusinessAccessLayer.Services

{

    public class CategoryServices : ICategory

    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)

        {

            _unitOfWork = unitOfWork;

        }

        public List<Category> GetAllCategories()

        {

            return _unitOfWork.Category.GetAll().ToList();

        }

        public Category GetCategoryById(int id)

        {

            return _unitOfWork.Category.Get(u => u.Id == id);

        }

        public void AddCategory(Category category)

        {

            _unitOfWork.Category.Add(category);

            _unitOfWork.Save();

        }

        public void UpdateCategory(Category category)

        {

            _unitOfWork.Category.Update(category);

            _unitOfWork.Save();

        }

        public void DeleteCategory(int id)

        {

            var category = _unitOfWork.Category.Get(u => u.Id == id);

            if (category != null)

            {

                _unitOfWork.Category.Remove(category);

                _unitOfWork.Save();

            }

        }

    }

}
