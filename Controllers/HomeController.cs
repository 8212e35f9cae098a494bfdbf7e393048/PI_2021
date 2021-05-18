using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCMissingPersonAppValid.Data;
using MVCMissingPersonAppValid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMissingPersonAppValid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return RedirectToAction("MissingPersons");
        }

        public IActionResult MissingPersons(int page = 1, char plec = ' ')
        {
            int count = dbContext.MissingPeople.Count();
            int pages = (int)Math.Ceiling(((double)count / 10));
            if (plec == 'M' || plec == 'K')
            {
                ViewBag.People = dbContext.MissingPeople
                .Where(p => p.Gender == plec)
                .Skip(10 * (page - 1))
                .Take(10)
                .ToList();
            }
            else
            {
                ViewBag.People = dbContext.MissingPeople
                .Skip(10 * (page - 1))
                .Take(10)
                .ToList();
            }
            ViewBag.Pages = pages;
            ViewBag.Gender = plec;
            ViewBag.Page = page;
            return View("Index");
        }


        public IActionResult Add()
        {
            var model = new PersonModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonModel personModel)
        {

            if (ModelState.IsValid)
            {

                if (!personModel.Picture.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("message", "Format zdjęcia jest niepoprawny!");
                }
                else
                {

                    var photosDir = Path.Combine(webHostEnvironment.WebRootPath, "zdjecia");
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(personModel.Picture.FileName);

                    personModel.Picture.CopyTo(new FileStream(Path.Combine(photosDir, filename), FileMode.Create));

                    MissingPerson person = new MissingPerson
                    {
                        FirstName = personModel.FirstName,
                        LastName = personModel.LastName,
                        Gender = personModel.Gender,
                        Age = personModel.Age,
                        Description = personModel.Description,
                        Picture = filename,
                        City = personModel.City
                    };
                    await dbContext.MissingPeople.AddAsync(person);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("MissingPersons", new { page = (int)Math.Ceiling(((double)dbContext.MissingPeople.Count() / 10)) });
                }
            }
            return View(personModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
