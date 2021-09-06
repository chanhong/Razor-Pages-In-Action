﻿using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityBreaks.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        [Display(Name = "Cities")]
        public int[] SelectedCities { get; set; }
        public SelectList Cities { get; set; }
        public string Message { get; set; }
        public async Task OnGetAsync()
        {
            Cities = await GetCityOptions();
        }
        public async Task OnPostAsync()
        {
            Cities = await GetCityOptions();
            if (ModelState.IsValid)
            {
                var cityIds = SelectedCities.Select(x => x.ToString());
                var cities = Cities.Where(o => cityIds.Contains(o.Value)).Select(o=>o.Text);
                Message = $"You selected {string.Join(", ", cities)}";
            }
        }
        private async Task<SelectList> GetCityOptions()
        {
            var service = new SimpleCityService();
            var cities = await service.GetCities();
            return new SelectList(cities, nameof(City.Id), nameof(City.Name));
        }
    }
}