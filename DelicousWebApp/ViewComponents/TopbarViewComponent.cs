using DelicousWebApp.DAL;
using DelicousWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DelicousWebApp.ViewComponents
{
    public class TopbarViewComponent:ViewComponent
    {
        private readonly DelicousDbContext _context;

        public TopbarViewComponent(DelicousDbContext delicousDbContext)
        {
            _context = delicousDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, Setting> setting = await _context.Settings.ToDictionaryAsync(k => k.Key);
            return View(setting);
        }
    }
}
