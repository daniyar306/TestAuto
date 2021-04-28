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
    public class driverController : Controller
    {
        private readonly driverRepository driverRepository;

        public driverController(IConfiguration configuration)
        {
            driverRepository = new driverRepository(configuration);
        }


        public IActionResult Index()
        {
            return View(driverRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

        // GET: /Customer/Edit/1
        public IActionResult Edit(int? id)
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
            return View(obj);

        }

        // POST: /Customer/Edit   
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

        // GET:/Customer/Delete/1
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
