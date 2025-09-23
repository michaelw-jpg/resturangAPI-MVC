using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturangAPI_MVC.Models;
using resturangAPI_MVC.ViewModel.Menu;

namespace resturangAPI_MVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly HttpClient _httpClient;
        public MenuController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }


        // GET: MenuController
        public async Task <ActionResult> Index()
        {
            var response = _httpClient.GetAsync("api/menus");
            var menuList = await response.Result.Content.ReadFromJsonAsync<List<Menu>>();
            return View(menuList);
        }

        // GET: MenuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            
            return View(new CreateMenuVM());
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Create(CreateMenuVM menu)
        {
            

            try
            {
                
                if (!ModelState.IsValid)
                {
                    return View(menu);
                }
                var response = await _httpClient.PostAsJsonAsync("api/menus", menu);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(menu);
            }
            return View(menu);
        }

        // GET: MenuController/Edit/5
        
        public async Task<ActionResult> Edit(int id)
        {
            var menu = await _httpClient.GetFromJsonAsync<Menu>($"api/menus/{id}");
            if (menu == null)
            {
                return NotFound();
            }
            var patchMenuVM = new PatchMenuVM
            {
                Name = menu.Name,
                Price = menu.Price,
                Description = menu.Description,
                IsPopular = menu.IsPopular,
                ImageUrl = menu.ImageUrl
            };

            ViewBag.MenuId = menu.MenuId;

            return View(patchMenuVM);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PatchMenuVM menu)
        {
            try
            {
                var response = await _httpClient.PatchAsJsonAsync($"api/menus/{id}", menu);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update menu");
                return View(menu);
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public async Task <ActionResult> Delete(int id)
        {
            var menu = await _httpClient.GetFromJsonAsync<Menu>($"api/menus/{id}");
            return View(menu);
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> DeleteConfirm(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/menus/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
