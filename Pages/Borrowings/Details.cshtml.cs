﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Munteanu_Paul_Lab2.Data;
using Munteanu_Paul_Lab2.Models;

namespace Munteanu_Paul_Lab2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Munteanu_Paul_Lab2.Data.Munteanu_Paul_Lab2Context _context;

        public DetailsModel(Munteanu_Paul_Lab2.Data.Munteanu_Paul_Lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            else
            {
                Borrowing = borrowing;
            }
            return Page();
        }
    }
}