using Microsoft.AspNetCore.Mvc;
using Module5Demo.Models.Home;

namespace Module5Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return View("ContactConfirmed", model.Comment);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
