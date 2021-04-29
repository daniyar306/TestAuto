using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuto.Models;
using TestAuto.Service;

namespace TestAuto.Controllers
{
    public class driverController : Controller
    {
        private readonly driverRepository driverRepository;
        private readonly autoRepository autoRepository;

        public driverController(IConfiguration configuration)
        {
            driverRepository = new driverRepository(configuration);
            autoRepository = new autoRepository(configuration);
        }


        public async Task<IActionResult> IndexAsync()
        {
            return View(await driverRepository.FindAllAsync());
        }

        public IActionResult Create()
        {
            ViewBag.auto =new SelectList(autoRepository.FindAll(), "id", "model"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(driver cust)
        {
            if (ModelState.IsValid)
            {
                driverRepository.Add(cust);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", driverRepository.FindAll()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_Index", cust) });


        }

        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            driver obj = driverRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.auto = new SelectList(autoRepository.FindAll(), "id", "model", obj.auto_id);
            return View(obj);

        }

        [HttpPost]
        public IActionResult Edit(driver obj)
        {

            if (ModelState.IsValid)
            {
               driverRepository.Update(obj);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", driverRepository.FindAll()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_Index", obj) });

        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            driverRepository.Remove(id.Value);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", driverRepository.FindAll()) });

        }
    }
}
