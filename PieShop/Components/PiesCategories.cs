using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Components
{
    public class PiesCategories : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public PiesCategories(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(categoryRepository.AllCategories);
        }
    }
}
