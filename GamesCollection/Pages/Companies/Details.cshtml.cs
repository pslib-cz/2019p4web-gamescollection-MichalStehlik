using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamesCollection.Models;

namespace GamesCollection.Companies
{
    public class DetailsModel : PageModel
    {
        private readonly GamesCollection.Models.ApplicationDbContext _context;

        public DetailsModel(GamesCollection.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company = await _context.Companies
                .Include(c => c.Parent).FirstOrDefaultAsync(m => m.Id == id);

            if (Company == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
