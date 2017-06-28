using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Module5Demo.Controllers
{
    public class CultureController : Controller
    {
        [HttpPost]
        public IActionResult Set(string uiCulture, string returnUrl)
        {
            IRequestCultureFeature feature = 
                HttpContext.Features.Get<IRequestCultureFeature>();

            RequestCulture requestCulture = 
                new RequestCulture(feature.RequestCulture.Culture, 
                                   new CultureInfo(uiCulture));

            string cookieValue = 
                CookieRequestCultureProvider.MakeCookieValue(requestCulture);

            string cookieName =
                CookieRequestCultureProvider.DefaultCookieName;

            Response.Cookies.Append(cookieName, cookieValue);

            return LocalRedirect(returnUrl);
        }
    }
}
