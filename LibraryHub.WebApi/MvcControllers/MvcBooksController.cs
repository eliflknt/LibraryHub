using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Services;
using LibraryHub.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.MvcControllers
{
    public class MvcBooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public MvcBooksController(
            IBookService bookService,
            ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllAsync(
                1,
                100,
                null,
                null);

            if (!result.IsSuccess || result.Data == null)
            {
                return View("~/Views/Books/Index.cshtml",
                    new List<BookViewModel>());
            }

            var books = result.Data.Items.Select(book => new BookViewModel
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Baslik,
                PublicationYear = book.YayinYili,
                StockQuantity = book.StokAdedi,
                RaftakiAdet = book.RaftakiAdet,
                CategoryId = book.CategoryId
            }).ToList();

            return View("~/Views/Books/Index.cshtml", books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Categories = categories;

            return View("~/Views/Books/Create.cshtml",
                new CreateBookViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();

                ViewBag.Categories = categories;

                return View("~/Views/Books/Create.cshtml", model);
            }

            var createBookDto = new CreateBookDto
            {
                ISBN = model.ISBN,
                Baslik = model.Title,
                YayinYili = model.PublicationYear,
                StokAdedi = model.StockQuantity,
                RaftakiAdet = model.RaftakiAdet,
                CategoryId = model.CategoryId
            };

            var result = await _bookService.CreateAsync(createBookDto);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.Error ?? "Kitap eklenirken bir hata oluştu.");

                var categories = await _categoryService.GetAllAsync();

                ViewBag.Categories = categories;

                return View("~/Views/Books/Create.cshtml", model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}