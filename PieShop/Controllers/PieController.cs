using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly ICategoryRepository categoryRepository;

        public PieController(IPieRepository pieRepository,ICategoryRepository categoryRepository)
        {
            this.pieRepository = pieRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult List(int? categoryId)
        {
            PieListViewModel pieListViewModel = new();
            pieListViewModel.Pies = pieRepository.AllPies.Where(p => categoryId != null ? p.CategoryId == categoryId : true);
            pieListViewModel.CurrentCategory = pieListViewModel.Pies.FirstOrDefault(p => p.CategoryId == categoryId)?.Category.CategoryName;
            return View(pieListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}
