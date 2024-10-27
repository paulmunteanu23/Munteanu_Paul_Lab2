﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Munteanu_Paul_Lab2.Models;
using Munteanu_Paul_Lab2.Data;
using Munteanu_Paul_Lab2.Models;

namespace Munteanu_Paul_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Munteanu_Paul_Lab2Context _context;

        public EditModel(Munteanu_Paul_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {   
                return NotFound();
            }

            var book = await _context.Book
                            .Include(b => b.Publisher)
                            .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }
            Book = book;

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "Id", "FullName");


            PopulateAssignedCategoryData(_context, book);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                            .Include(i => i.Publisher)
                            .Include(i => i.BookCategories)
                            .ThenInclude(i => i.Category)
                            .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.Author,
                i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, bookToUpdate);
                return Page();
            }

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}