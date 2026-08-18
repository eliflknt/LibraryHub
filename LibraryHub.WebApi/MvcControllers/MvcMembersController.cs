using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Services;
using LibraryHub.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.MvcControllers
{
    public class MvcMembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MvcMembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _memberService.GetAllAsync();

            if (!result.IsSuccess || result.Data == null)
            {
                return View("~/Views/Members/Index.cshtml",
                    new List<MemberViewModel>());
            }

            var members = result.Data.Select(member => new MemberViewModel
            {
                Id = member.Id,
                AdSoyad = member.AdSoyad,
                Email = member.Email,
                Telefon = member.Telefon,
                UyelikTarihi = member.UyelikTarihi,
                AktifMi = member.AktifMi
            }).ToList();

            return View("~/Views/Members/Index.cshtml", members);
        }
    }
}