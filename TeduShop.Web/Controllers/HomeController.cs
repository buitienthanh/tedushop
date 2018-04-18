﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;
        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _commonService = commonService;
        }
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable< SlideViewModel> >(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;

            var lasteProductModel = _productService.GetLastest(3);
            var topSaleProductModel= _productService.GetHotProduct(3);
            var lasteProductView= Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lasteProductModel);
            var topSaleProductView = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.LastestProduct = lasteProductView;
            homeViewModel.TopSaleProduct = topSaleProductView;
            return View(homeViewModel);
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

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer,FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}