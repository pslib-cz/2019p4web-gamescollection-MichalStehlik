﻿using System;
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
        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGet(string order, string search, string nameFilter, string countryFilter, int? ownerFilter = null)
        {
            CountriesList = new SelectList(new List<string> { 
                "CZ", "FR", "GE", "PL", "SE", "US"
            });
            IQueryable<Company> companies = _context.Companies;
            if (nameFilter != null)
            {
                companies = companies.Where((c) => (c.Name.Contains(nameFilter)));
            }
            if (countryFilter != null)
            {
                companies = companies.Where((c) => (c.CountryCode == countryFilter));
            }
            if (ownerFilter != null)
            {
                if (ownerFilter != 0)
                {
                    companies = companies.Where((c) => (c.ParentId == ownerFilter));
                }
                else
                {
                    companies = companies.Where((c) => (c.ParentId == null));
                }
            }
            Companies = companies.ToList();
        }
    }
}
