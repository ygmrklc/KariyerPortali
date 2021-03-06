﻿using AutoMapper;
using KariyerPortali.Admin.Models;
using KariyerPortali.Admin.ViewModels;
using KariyerPortali.Model;
using KariyerPortali.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerPortali.Admin.Controllers
{
    public class PageController : BaseController
    {
        // GET: Pages
       private readonly IPageService pageService;
       

       public PageController(IPageService pageService)
        {
            this.pageService = pageService;
            
        }

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Edit(int? id)
        {
           
            if (id.HasValue)
            {
                var Page = pageService.GetPage(id.Value);
                if (Page != null)
                {
                    var PageViewModel = Mapper.Map<Page, PageViewModel>(Page);
                    return View(PageViewModel);
                }

            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PageFormViewModel PageForm)
        {
            if (ModelState.IsValid)
            {
                var Page = Mapper.Map<PageFormViewModel, Page>(PageForm);
                pageService.UpdatePage(Page);
                pageService.SavePage();
                return RedirectToAction("Index");
            }
            return View(PageForm);
        }

      
        public ActionResult Create()
        {
            ViewBag.ParentPageId = new SelectList(pageService.GetPages(), "ParentPageId", "ParentPage");
           
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageFormViewModel PageForm)
        {
            if (ModelState.IsValid)
            {
                var page = Mapper.Map<PageFormViewModel, Page>(PageForm);
                page.CreatedBy = User.Identity.Name;
                page.CreateDate = DateTime.Now;
                page.UpdatedBy = User.Identity.Name;
                page.UpdateDate = DateTime.Now;
                pageService.CreatePage(page);
                pageService.SavePage();
                return RedirectToAction("Index");
            }
            return View(PageForm);
        }

        public ActionResult Details(int id)
        {
            var Page = pageService.GetPage(id);
            return View(Mapper.Map<Page, PageViewModel>(Page));

        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                var Page = pageService.GetPage(id.Value);
                if (Page != null)
                {
                    pageService.DeletePage(Page);
                    pageService.SavePage();
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }
     

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {

            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedPages = pageService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);

            var result = from p in displayedPages
                         select new[] { p.PageId.ToString(), p.Title.ToString(),p.CreatedBy.ToString(),p.ViewCount.ToString(),p.UpdateDate.ToString(),string.Empty };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = result.ToList()
            },
                JsonRequestBehavior.AllowGet);
        }
    }
}