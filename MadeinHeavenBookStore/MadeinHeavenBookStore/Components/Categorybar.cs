using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Components
{
    public class Categorybar : ViewComponent
    {
        private readonly MadeinHeavenBookStoreContext _Context;
        public Categorybar(MadeinHeavenBookStoreContext context)
        {
            _Context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_Context.Categories.ToList());
        }
    }
}
