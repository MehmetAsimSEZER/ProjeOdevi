using Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Models.Components
{
    public class SmallCartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            SmallCartVM model;

            if (cart == null || cart.Count == 0)
                model = null;
            else
            {
                model = new SmallCartVM()
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }
            return View(model);
        }
    }
}
