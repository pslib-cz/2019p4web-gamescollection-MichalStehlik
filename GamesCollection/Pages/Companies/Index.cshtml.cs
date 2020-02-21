using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamesCollection.Models;

namespace GamesCollection
{
    public class IndexModel : PageModel
    {
        private readonly GamesCollection.Models.ApplicationDbContext _context;

        public IndexModel(GamesCollection.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Company> Company { get;set; }

        public async Task OnGetAsync()
        {
            Company = await _context.Companies
                .Include(c => c.Parent).ToListAsync();
        }
    }
}
