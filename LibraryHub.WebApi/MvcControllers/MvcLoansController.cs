using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Services;
using LibraryHub.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.MvcControllers
{
    public class MvcLoansController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;

        public MvcLoansController(
            ILoanService loanService,
            IBookService bookService,
            IMemberService memberService)
        {
            _loanService = loanService;
            _bookService = bookService;
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Borrow()
        {
            await LoadBooksAndMembers();

            return View(
                "~/Views/Loans/Borrow.cshtml",
                new BorrowBookViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(BorrowBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadBooksAndMembers();

                return View(
                    "~/Views/Loans/Borrow.cshtml",
                    model);
            }

            var result = await _loanService.BorrowBookAsync(
                model.MemberId,
                model.BookId);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.Error ?? "Ödünç verme işlemi başarısız oldu.");

                await LoadBooksAndMembers();

                return View(
                    "~/Views/Loans/Borrow.cshtml",
                    model);
            }

            TempData["SuccessMessage"] =
                "Kitap başarıyla ödünç verildi.";

            return RedirectToAction(nameof(Borrow));
        }

        private async Task LoadBooksAndMembers()
        {
            var booksResult = await _bookService.GetAllAsync();
            var membersResult = await _memberService.GetAllAsync();

            ViewBag.Books = booksResult.Data?.ToList()
                ?? new List<LibraryHub.Application.DTOs.BookDto>();

            ViewBag.Members = membersResult.Data?.ToList()
                ?? new List<LibraryHub.Application.DTOs.MemberDto>();
        }
    }
}