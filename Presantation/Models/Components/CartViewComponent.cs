using Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Models.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartVM model;

            if (cart == null || cart.Count == 0)
            {
                model = new CartVM()
                {
                    NumberOfItems = 0,
                };
            }
            else
            {
                model = new CartVM()
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                };
            }
            return View(model);
        }
    }
}
