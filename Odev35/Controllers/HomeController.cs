using Microsoft.AspNetCore.Mvc;
using Odev35.Models;
using System.Diagnostics;
using System.Globalization;

namespace YourProjectNamespace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UnluUyumuKontrolu(string kelime)
        {
            bool unluUyumuVarMi = KelimeUnluUyumuKontrolu(kelime);
            return Json(unluUyumuVarMi);
        }

        private bool KelimeUnluUyumuKontrolu(string kelime)
        {
            if (string.IsNullOrEmpty(kelime))
            {
                return true;
            }

            char sonUnlu = SonUnluGetir(kelime);

            if ("aeıioöuü".Contains(sonUnlu))
            {
                return true;
            }
            else if ("ou".Contains(sonUnlu))
            {
                return "ae".Contains(kelime.ToLower()[0]);
            }
            else if ("aı".Contains(sonUnlu))
            {
                return "aeı".Contains(kelime.ToLower()[0]);
            }

            return false;
        }

        private char SonUnluGetir(string kelime)
        {
            CultureInfo trCulture = new CultureInfo("tr-TR");
            TextInfo textInfo = trCulture.TextInfo;
            kelime = textInfo.ToLower(kelime);

            for (int i = kelime.Length - 1; i >= 0; i--)
            {
                if ("aeıioöuü".Contains(kelime[i]))
                {
                    return kelime[i];
                }
            }

            return ' ';
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
