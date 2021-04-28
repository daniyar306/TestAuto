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


        public IActionResult Index()
        {
            return View(autoRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(auto cust)
        {
            if (ModelState.IsValid)
            {
                autoRepository.Add(cust);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", autoRepository.FindAll()) });

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
            auto obj = autoRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Customer/Edit   
        [HttpPost]
        public IActionResult Edit(auto obj)
        {

            if (ModelState.IsValid)
            {
                autoRepository.Update(obj);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", autoRepository.FindAll()) });
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
            autoRepository.Remove(id.Value);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Index", autoRepository.FindAll()) });

        }
    }
}
