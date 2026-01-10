using Microsoft.AspNetCore.Mvc;
using Project.Bll.Managers.Abstracts;
using Project.Entities.Models;
using System.Threading.Tasks;

namespace Project.MvcUI.Areas.Admin.Controllers
{
    //Refactorler proje tamamlandıktan sonra olan ödevlerdir...
    //Todo : Authorization

    //Validation refactorlerini proje bittikten sonra ekleyiniz...

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> Index()
        {

            //Todo : Refactors :  Manager refactor edilerek Dto'lar ile calısılması saglanmalı
            //Action'in da gelen DTO'yu View'a maplemesi saglanmalı

            //PageVm olmalı
            List<Category> categories = await _categoryManager.GetAllAsync();
             
            return View(categories);
        }

        public IActionResult Create()
        {
          return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            await _categoryManager.CreateAsync(model);
            return RedirectToAction("Index");
        }

        //Todo : Category Crud tamamlanacak ve sonra Product Crud'a gecilecek...
    }
}
