using BulkyBook.Models;

namespace BusinessAccessLayer.Interfaces

{

    public interface ICategory

    {

        List<Category> GetAllCategories();

        Category GetCategoryById(int id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(int id);

    }

}
