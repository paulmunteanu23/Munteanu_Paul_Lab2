using Munteanu_Paul_Lab2.Models;
namespace Munteanu_Paul_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
