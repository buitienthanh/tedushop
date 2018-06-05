using AutoMapper;
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
        IPageService _pageService;
        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService, IProductService productService, IPageService pageService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _commonService = commonService;
            _pageService = pageService;
        }

        [OutputCache(Duration = 3600,Location=System.Web.UI.OutputCacheLocation.Client)]
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

      

        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer,FooterViewModel>(footerModel);
            ViewBag.Time = DateTime.Now.ToString("T");
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600)]
        public ActionResult Header()
        {
            var pageModel = _pageService.GetAll();
            var model = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(pageModel);
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}