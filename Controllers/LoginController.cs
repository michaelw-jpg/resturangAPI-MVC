using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturangAPI_MVC.ViewModel.Login;
using System.Security.Claims;
using System.Text.Json;

namespace resturangAPI_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }


        public ActionResult Index()
        {
            return View();
        }

        public async Task <ActionResult> Logout()
        {
            Response.Cookies.Delete("jwtToken");
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Index(LoginVM loginRequest)
        {
           if(ModelState.IsValid)
            {
                var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
               
                if(result.IsSuccessStatusCode)
                {
                  
                   
                   var claims = new List<Claim>
                   {
                       new Claim(ClaimTypes.Name, loginRequest.Name)
                   };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("CookieAuth", principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1)
                    });

                  if(result.Headers.TryGetValues("set-Cookie", out var cookies))
                    {
                        foreach(var cookie in cookies)
                        {
                            var cookiePart = cookie.Split(";")[0];
                            var name = cookiePart.Split("=")[0];
                            var token = cookiePart.Split("=")[1];

                            Response.Cookies.Append(name, token, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTime.UtcNow.AddHours(1)
                            });
                        }
                    }

                }
                else if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(loginRequest);
                }
              return RedirectToAction("Index", "Home");
           }
           ModelState.AddModelError("", "Invalid login attempt.");
            return View(loginRequest);
        }
        public class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}
