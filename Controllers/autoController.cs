using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuto.Models;
using TestAuto.Service;

namespace TestAuto.Controllers
{
    public class autoController : Controller
    {
        private readonly autoRepository autoRepository;

        public autoController(IConfiguration configuration)
        {
            autoRepository = new autoRepository(configuration);
        }


        public async Task<IActionResult> IndexAsync()
        {
            return View(await autoRepository.FindAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        public  IActionResult CreateAsync(auto cust)
        {
            if (ModelState.IsValid)
            {
                autoRepository.Add(cust);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index",  autoRepository.FindAll()) });

            }
          
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_Index", cust) });
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            auto obj = autoRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(auto obj)
        {

            if (ModelState.IsValid)
            {
                await autoRepository.UpdateAsync(obj);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index",autoRepository.FindAll()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_Index", obj) });
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            autoRepository.Remove(id.Value);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", autoRepository.FindAll()) });

        }
    }
}
