using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesCollection.Companies
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private ApplicationDbContext _context;

        public DetailsModel(ILogger<DetailsModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Company Company { get; set; }
        public List<Game> GamesPublished { get; set; } = new List<Game>();
        public List<Game> GamesDeveloped { get; set; } = new List<Game>();
        public List<Company> Subsidiaries { get; set; } = new List<Company>();

        public void OnGet(int id)
        {
            Company = _context.Companies.Include(c => c.Parent).Where(c=>(c.Id == id)).FirstOrDefault();
            GamesPublished = _context.Games.Where(g => (g.PublisherId == id)).OrderBy(g => g.Name).ToList();
            GamesDeveloped = _context.Games.Where(g => (g.DeveloperId == id)).OrderBy(g => g.Name).ToList();
            Subsidiaries = _context.Companies.Where(c => (c.ParentId == id)).OrderBy(c => c.Name).ToList();
        }
    }
}