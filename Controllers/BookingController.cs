using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturangAPI_MVC.Models;
using resturangAPI_MVC.ViewModel.Booking;

namespace resturangAPI_MVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }
        // GET: BookingController
        [Authorize]
        public async Task <ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/bookings");
            if (!response.IsSuccessStatusCode)
            {
                // Log or handle the error
                var errorText = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API error response: " + errorText);
                return View(new List<Booking>());
            }

            var bookingList = await response.Content.ReadFromJsonAsync<List<Booking>>();
            bookingList = bookingList.OrderByDescending(b => b.BookingTime).ToList();
            return View(bookingList);
        }

        // GET: BookingController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookingController/Create
        public ActionResult Create(int? tableId)
        {
            var viewModel = new CreateBookingVM();
            if (tableId.HasValue)
            {
                viewModel.TableId_FK = tableId.Value;
            }
            var apiBaseUrl = _httpClient.BaseAddress?.ToString() ?? "";
            ViewData["ApiBaseUrl"] = apiBaseUrl;
            return View(viewModel);
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Create(CreateBookingVM booking)
        {
            if (!ModelState.IsValid)
            {
                var apiBaseUrl = _httpClient.BaseAddress?.ToString() ?? "";
                ViewData["ApiBaseUrl"] = apiBaseUrl;
                return View(booking);
            }
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/bookings", booking);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View(booking);
            }
            return View(booking);
        }

        // GET: BookingController/Edit/5
        [Authorize]
        public async Task <ActionResult> Edit(int id)
        {
            var booking = await _httpClient.GetFromJsonAsync<Booking>($"api/bookings/{id}");

            var PatchBooking = new PatchBookingVM
            {
                Name = booking.Name,
                PhoneNumber = booking.PhoneNumber,
                Email = booking.Email,
                TableId_FK = booking.TableId_FK,
                Guests = booking.Guests,
                BookingTime = booking.BookingTime
            };
            ViewBag.BookingId = booking.BookingId;

            return View(PatchBooking);
        }

        // POST: BookingController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Delete/5
        [Authorize]
        public async Task <ActionResult> Delete(int id)
        {

            var booking = await _httpClient.GetFromJsonAsync<Booking>($"api/bookings/{id}");
            return View(booking);
        }

        // POST: BookingController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> DeleteConfirm(int id, IFormCollection collection)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/bookings/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
