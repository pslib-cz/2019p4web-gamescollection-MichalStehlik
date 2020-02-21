using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesCollection.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private ApplicationDbContext _context;

        public IList<Company> Companies { get; set; }
        public SelectList CountriesList { get; set; }
        [BindProperty(SupportsGet = true)]
        public CompaniesSortOrder Order { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CountryFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? OwnerFilter { get; set; }
        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGet()
        {
            CountriesList = new SelectList(new List<string> { 
                "CZ", "FR", "GE", "PL", "SE", "US"
            });
            IQueryable<Company> companies = _context.Companies;
            if (NameFilter != null)
            {
                companies = companies.Where((c) => (c.Name.Contains(NameFilter)));
            }
            if (CountryFilter != null)
            {
                companies = companies.Where((c) => (c.CountryCode == CountryFilter));
            }
            if (OwnerFilter != null)
            {
                if (OwnerFilter != 0)
                {
                    companies = companies.Where((c) => (c.ParentId == OwnerFilter));
                }
                else
                {
                    companies = companies.Where((c) => (c.ParentId == null));
                }
            }
            switch (Order)
            {
                case CompaniesSortOrder.Name: 
                    companies = companies.OrderBy(c => c.Name); break;
                case CompaniesSortOrder.NameDescending: 
                    companies = companies.OrderByDescending(c => c.Name); break;
                case CompaniesSortOrder.Country: 
                    companies = companies.OrderBy(c => c.CountryCode); break;
                case CompaniesSortOrder.CountryDescending: 
                    companies = companies.OrderByDescending(c => c.CountryCode); break;
            }
            Companies = companies.ToList();
        }
    }

    public enum CompaniesSortOrder
    {
        None,
        Name,
        NameDescending,
        Country,
        CountryDescending
    }
}
