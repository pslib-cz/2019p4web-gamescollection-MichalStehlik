using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesCollection
{
    public class GameModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private ApplicationDbContext _context;
        public Game Game { get; set; }
        public GameModel(ILogger<DetailsModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnGet(int id)
        {
            Game = _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Where(g => (g.Id == id)).FirstOrDefault();
        }
    }
}