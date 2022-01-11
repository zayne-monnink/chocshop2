using ChocShop2.DAL;
using ChocShop2.DAL.Entities;
using ChocShop2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace ChocShop2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Index()
        {
            // JUST TESTING
            //ravendbtest db = new ravendbtest();
            //db.TestConnection();
            // JUST TESTING

            ProductRepository productRepository = new ProductRepository();
            var products = productRepository.GetProducts();

            var model = new ProductsListViewModel { Products = new List<ProductsListViewModel.ListProductModel>() };
            foreach (var product in products)
            {
                model.Products.Add(new ProductsListViewModel.ListProductModel
                {
                    Id = product.Id,
                    Name = product.ProductName,
                    Price = product.Price,
                });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Product(string id, bool addedToBasket = false)
        {
            ProductRepository productRepository = new ProductRepository();
            string urlDecodedProductId = WebUtility.UrlDecode(id);
            var product = productRepository.GetProduct(urlDecodedProductId);

            var model = new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.ProductName,
                Price = product.Price,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            // This page gets the list of basket items from javascript session storage
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutSubmitModel model)
        {
            // Process the payment using the CC details and redirect the user to the OrderComplete() method to show the result

            OrderRepository orderRepository = new OrderRepository();
            ProductRepository productRepository = new ProductRepository();


            Order order = new Order();
            order.StatusId = 1; // 1 = new order
            order.OverallTotal = 0;
            order.OrderLines = new List<Order_OrderLines>();
            order.OrderNumber = DateTime.Now.Ticks.ToString();

            for (int i = 0; i < model.ProductIds.Count; i++)
            {
                var productId = model.ProductIds[i];
                var qty = model.Quantities[i];

                var product = productRepository.GetProduct(productId);
                order.OrderLines.Add(new Order_OrderLines
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ProductPrice = product.Price,
                    ProductQuantity = qty,
                });
                order.OverallTotal = order.OverallTotal + (product.Price * qty);
            }

            orderRepository.CreateOrder(order);

            string orderNumber = order.OrderNumber;

            return RedirectToAction(nameof(OrderComplete), new { orderNumber = orderNumber });
        }

        [HttpGet]
        public IActionResult OrderComplete(string orderNumber)
        {
            OrderCompleteViewModel model = new OrderCompleteViewModel();
            model.OrderNumber = orderNumber;

            return View(model);
        }

    }
}