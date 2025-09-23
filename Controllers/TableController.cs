using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturangAPI_MVC.Models;
using resturangAPI_MVC.ViewModel.Menu;
using resturangAPI_MVC.ViewModel.Table;

namespace resturangAPI_MVC.Controllers
{
    public class TableController : Controller
    {
        private readonly HttpClient _httpClient;
        public TableController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        // GET: TableController
        public async Task <ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Tables");

            if (!response.IsSuccessStatusCode)
            {
                
                return View("Error");
            }
                

            var tableList = await response.Content.ReadFromJsonAsync<List<Table>>();
            return View(tableList);
        }

        
        public async Task <ActionResult> BookingsPerTable(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Bookings/bookingsbytable/{id}");

            var bookingList = await response.Content.ReadFromJsonAsync<List<Booking>>();
            ViewBag.TableId = id;

            return View(bookingList);
        }

        // GET: TableController/Create
        public ActionResult Create()
        {
            return View(new CreateTableVM ());
        }

        // POST: TableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Create(CreateTableVM table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Tables", table);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(table);
            }
            return View(table);
        }

        // GET: TableController/Edit/5
        public async Task <ActionResult> Edit(int id)
        {
            var table = await _httpClient.GetFromJsonAsync<Table>($"api/tables/{id}");
            if (table == null)
            {
                return NotFound();
            }
            var patchTableVM = new PatchTableVM
            {
                TableNumber = table.TableNumber,
                Seats = table.Seats
            };
            

            ViewBag.TableId = table.TableId;

            return View(patchTableVM);
        }

        // POST: TableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int id, PatchTableVM table)
        {
            try
            {
                var response = await _httpClient.PatchAsJsonAsync($"api/tables/{id}", table);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update table");
                return View(table);
            }
            catch
            {
                return View();
            }
        }
        

        // GET: TableController/Delete/5
        public async Task <ActionResult> Delete(int id)
        {
            var table = await _httpClient.GetFromJsonAsync<Table>($"api/tables/{id}");
            return View(table);
        }

        // POST: TableController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> DeleteConfirm(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/tables/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
