using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Dtos.CategoriesDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Test.Controllers
{
    [Authorize (Roles = "Admin")]
    public class CategoryController : Controller

    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _categoryService.GetCategories();
            return View(res);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategorDto category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var res = await _categoryService.GetCategory(Id);
            if (res == null)
            {
                return NotFound();
            }
            var category = new PostCategoryDto
            {
                Id = res.Id,
                Name = res.CategoryName,
                Description = res.Description,
            };
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var res = await _categoryService.GetCategory(Id);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            await _categoryService.DeleteCategory(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            var res = await _categoryService.GetCategory(Id);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }
    }
}
