using LibraryApplication.Models;
using LibraryApplication.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers
{
    [Authorize(Roles = "Admin,Ogrenci")]  // Admin ve Öğrenci rolleri için erişim izni
    public class KitapListesiController : Controller
    {
        private readonly IKitapRepository _kitapRepository;

        public KitapListesiController(IKitapRepository kitapRepository)
        {
            _kitapRepository = kitapRepository;
        }

        // Kitapları listeleme işlemi
        public IActionResult Index()
        {
            // Kitapları al ve listele
            List<Kitap> kitaplar = _kitapRepository.GetAll(includeProps: "KitapTuru").ToList();
            return View(kitaplar);
        }
    }
}
