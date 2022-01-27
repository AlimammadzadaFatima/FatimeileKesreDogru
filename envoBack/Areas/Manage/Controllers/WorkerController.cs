using envoBack.Helper;
using envoBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envoBack.Areas.Manage.Controllers
{[Area("manage")]
    [Authorize]
    public class WorkerController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public WorkerController(DataContext context, IWebHostEnvironment env)
        {
           _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Workers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            Worker worker = _context.Workers.FirstOrDefault(x => x.Id == id);
            if (worker == null) return NotFound();
            return View(worker);
        }
        public IActionResult Delete(int id)
        {
            Worker worker = _context.Workers.FirstOrDefault(x => x.Id == id);
            if (worker == null) return NotFound();
            if (!string.IsNullOrWhiteSpace(worker.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/worker", worker.Image);
            }
            _context.Remove(worker);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Worker worker)
        {
            if (!ModelState.IsValid) return View();
            Worker existWorker = _context.Workers.FirstOrDefault(x => x.Id == worker.Id);
            if (existWorker == null) return NotFound();
            if (worker.ImageFile != null)
            {
              
                if (worker.ImageFile.Length > 2097152) {
                    ModelState.AddModelError("ImageFile", "Sekil max 2mb");
                    return View();
                }
                if (worker.ImageFile.ContentType != "image/png" && worker.ImageFile.ContentType != "image/jpeg")
                { ModelState.AddModelError("ImageFile", "Sekil formati");
                    return View();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/worker", existWorker.Image);
                existWorker.Image = FileManager.Save(_env.WebRootPath, "uploads/worker", worker.ImageFile);
            }
           
            existWorker.FullName = worker.FullName;
            existWorker.Text = worker.Text;
            existWorker.Profession = worker.Profession;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Worker worker)
        {
            if (!ModelState.IsValid) return View();
           if(worker.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Sekil mutleq");
                return View();
            }
            if (worker.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Sekil max 2mb");
                return View();
            }
            if (worker.ImageFile.ContentType != "image/png" && worker.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Sekil formati");
                return View();
            }
          worker.Image = FileManager.Save(_env.WebRootPath, "uploads/worker", worker.ImageFile);
            _context.Add(worker);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
    
}
