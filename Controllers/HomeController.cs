using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestBook.Data;
using TestBook.Helper;
using TestBook.Models;

namespace TestBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookDbContext _bookDb;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, BookDbContext bookDb, IHttpContextAccessor httpContextAccessor)
        {
            _bookDb = bookDb;
            _logger = logger;
            this._httpContextAccessor = httpContextAccessor;
        }

        public List<Item> MyCart;
        public IActionResult Index()
        {
            var bookList = _bookDb.Books.ToList();

            return View(bookList);
        }

        public IActionResult OnGet()
        {
           
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Book.Price * item.Quantity);
                return View();
               
            }
            else
                ViewBag.Empty = "Empty Cart";
            return View();
        }

        public IActionResult onGetBuy(int id)
        {
            ViewBag.ID = id;
            var product = _bookDb.Books.Find(id);
            //  string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<Item>();
                cart.Add(new Item()
                {
                    Book = product,
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new Item()
                    {
                        Book = product,
                        Quantity = 1
                    });
                }

                else
                {
                    var newQuantity = cart[index].Quantity + 1;
                    cart[index].Quantity = newQuantity;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("OnGet"); 
        }
        public int Exists(List<Item> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Book.BookId.Equals(id))
                {
                    return i;
                }

            }
            return -1;
        }
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(cart,id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("OnGet");
        }

        public IActionResult Checkout()
        {
       
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
