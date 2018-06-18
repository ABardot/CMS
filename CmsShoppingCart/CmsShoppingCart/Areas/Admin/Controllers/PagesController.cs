using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            // Declare List of view PageVm
            List<PageVM> pagesList;
           
            using (Db db = new Db())
            {
                // Initiate the list, new PageVM is sent by the ctor in the controller. ToArrya is needed or Entity
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }

            // Return view with list

            return View(pagesList);
        }


        // GET: Admin/Pages/AddPage
        public ActionResult AddPage()
        {
            return View();
        }

        // GET: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
            // Check Model State
            if (! ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {


                // Declare Slug
                string slug;
                
                // Initiate pageDTO
                PageDTO dto = new PageDTO();

                // DTO Title
                dto.Title = model.Title;

                // Check for and set slug are if need be
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                // Make sure title and slug are unique
                if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or slug already exists");
                    return View(model);
                }

                // DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;
                dto.Sorting = 100;

                // Save DBO
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "You have added a new page!";

            // Redirect
            return RedirectToAction("AddPage");
        }

        public ActionResult EditPage(int id)
        {
            // Declare pageVM

            // Get the page

            // Confirm page exists

            // Initialize PageVm


            // Return View with Model
            return View();
        }
    }
}