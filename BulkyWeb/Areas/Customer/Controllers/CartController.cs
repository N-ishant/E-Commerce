using BulkyBook.Models.ViewModel;
using BusinessAccessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICart CartService;

        public CartController(ICart cartService)
        {
            CartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ShoppingCartVM = CartService.GetShoppingCart(userId);
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Plus(int cartId)
        {
            CartService.IncreaseQuantity(cartId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            CartService.DecreaseQuantity(cartId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            CartService.RemoveItem(cartId);
            return RedirectToAction(nameof(Index));
        }
    }
}