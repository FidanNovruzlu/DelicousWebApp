using DelicousWebApp.DAL;
using DelicousWebApp.Models;
using DelicousWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DelicousWebApp.Areas.Admin.Controllers;
[Area("Admin")]
public class SettingController : Controller
{
    private readonly DelicousDbContext _context;

    public SettingController(DelicousDbContext delicousDbContext)
    {
        _context = delicousDbContext;
    }
    public IActionResult Index()
    {
        List<Setting> settingList = _context.Settings.ToList();
        return View(settingList);
    }
    public IActionResult Update(int id)
    {
        Setting? setting = _context.Settings.Find(id);
        if (setting == null) return NotFound();

        UpdateSettingVM settingVM = new UpdateSettingVM()
        {
            Value = setting.Value,
        };
        return View(settingVM);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id,UpdateSettingVM settingVM)
    {
        if (!ModelState.IsValid) return View();

        Setting? setting = _context.Settings.Find(id);
        if (setting == null) return NotFound();

        setting.Value = settingVM.Value;
         _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
