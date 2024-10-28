using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Munteanu_Paul_Lab2.Data;
using Munteanu_Paul_Lab2.Models;
using Munteanu_Paul_Lab2.Models.ViewModels;

namespace Munteanu_Paul_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Munteanu_Paul_Lab2.Data.Munteanu_Paul_Lab2Context _context;

        public IndexModel(Munteanu_Paul_Lab2.Data.Munteanu_Paul_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Category = await _context.Category
            .Include(i => i.BookCategories)
            .ThenInclude(c => c.Book)
            .ThenInclude(c => c.Author)
            .OrderBy(i => i.CategoryName)
            .ToListAsync();
            if (id != null)
            {
                CategoryID = id.Value;
               Category category = CategoryData.Category
                .Where(i => i.ID == id.Value).Single();
                CategoryData.Books = category.BookCategories.Select(b => b.Book);
            }
        }
    }
}
