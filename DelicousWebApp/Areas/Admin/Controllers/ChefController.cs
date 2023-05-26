using DelicousWebApp.DAL;
using DelicousWebApp.Models;
using DelicousWebApp.ViewModels;
using DelicousWebApp.ViewModels.ChefVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DelicousWebApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]
public class ChefController : Controller
{
    private readonly DelicousDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ChefController(DelicousDbContext delicousDbContext,IWebHostEnvironment webHostEnvironment)
    {
        _context = delicousDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task< IActionResult> Index(int page=1,int take=5)
    {
        List<Chef> chefs =await _context.Chefs.Skip((page-1)*take).Take(take).Include(c => c.Job).ToListAsync();
        int AllPageCount = _context.Chefs.Count();
       PaginationVM<Chef> updateChefVM = new PaginationVM<Chef>()
       {
           CurrentPage=page,
           Chefs=chefs,
           PageCount=(int)(Math.Ceiling((double) AllPageCount/take))
       };
        return View(updateChefVM);
    }
    public IActionResult Create()
    {
       
        CreateChefVM createChefVM = new CreateChefVM()
        {
           Jobs=_context.Jobs.ToList()
        };
        return View(createChefVM);
    }
    [HttpPost]
    public async Task< IActionResult> Create(CreateChefVM createChefVM)
    {
        if (!ModelState.IsValid)
        {
             createChefVM.Jobs = await _context.Jobs.ToListAsync();
            return View(createChefVM);
        }

        Chef chef=new Chef()
        {
            Name=createChefVM.Name,
            Surname=createChefVM.Surname,
            JobId=createChefVM.JobId
        };

        if(!createChefVM.Image.ContentType.Contains("image/") & createChefVM.Image.Length / 1024 > 2048)
        {
            ModelState.AddModelError("", "Incorrect image type or size!");
            createChefVM.Jobs = await _context.Jobs.ToListAsync();
            return View(createChefVM);
        }

        string newFilename= Guid.NewGuid().ToString() + createChefVM.Image.FileName;
        string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","img","chefs",newFilename);
        using(FileStream file=new FileStream(path, FileMode.CreateNew))
        {
           await createChefVM.Image.CopyToAsync(file);
        }
        chef.ImageName = newFilename;

        await _context.Chefs.AddAsync(chef);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Update(int id)
    {
        Chef? chef = _context.Chefs.Include(c=>c.Job).FirstOrDefault(f=>f.Id==id);
        if(chef == null)  return NotFound();
     
        UpdateChefVM updateChefVM = new UpdateChefVM()
        {
            Name = chef.Name,
            Surname = chef.Surname,
            JobId = chef.JobId,
           ImageName = chef.ImageName,
           Jobs=_context.Jobs.ToList()
        };
        
        return View(updateChefVM);
    }
    [HttpPost]
    public IActionResult Update(int id, UpdateChefVM updateChefVM)
    {
        if (!ModelState.IsValid)
        {
            updateChefVM.Jobs =  _context.Jobs.ToList();
            return View(updateChefVM);
        }

        Chef? chef = _context.Chefs.Find(id);
        if (chef == null) return NotFound();
       
        if (!updateChefVM.Image.ContentType.Contains("image/") & updateChefVM.Image.Length / 1024 > 2048)
        {
            ModelState.AddModelError("", "Incorrect image type or size!");
            updateChefVM.Jobs =  _context.Jobs.ToList();
            return View(updateChefVM);
        }
        if(updateChefVM.Image!= null)
        {

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "chefs", chef.ImageName);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                updateChefVM.Image.CopyTo(file);
            }
        }
        chef.Name = updateChefVM.Name;
        chef.Surname = updateChefVM.Surname;
        chef.JobId = updateChefVM.JobId;
        chef.Id = id;

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Read(int id)
    {
        Chef? chef = _context.Chefs.Include(c=>c.Job).FirstOrDefault(f=>f.Id==id);
        if (chef == null) return NotFound();
        return View(chef);
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        Chef? chef = _context.Chefs.FirstOrDefault(f => f.Id == id);
        if (chef == null) return NotFound();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "chefs", chef.ImageName);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Chefs.Remove(chef);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
