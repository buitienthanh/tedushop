using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/footer")]
    public class FooterController : ApiControllerBase
    {
        private ICommonService _commonService;

        public FooterController(IErrorService errorService, ICommonService commonService)
            : base(errorService)
        {
            this._commonService = commonService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _commonService.GetAll();

                totalRow = model.Count();
                var query = model.Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Footer>, IEnumerable<FooterViewModel>>(query);

                var paginationSet = new PaginationSet<FooterViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, FooterViewModel footerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newFooter = new Footer();
                    newFooter.UpdateFooter(footerVm);
                    _commonService.Add(newFooter);
                    _commonService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(newFooter);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _commonService.GetById(id);

                var responseData = Mapper.Map<Footer, FooterViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, FooterViewModel footerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbFooter = _commonService.GetById(footerVm.ID);
                    dbFooter.UpdateFooter(footerVm);
                    _commonService.Update(dbFooter);
                    _commonService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(dbFooter);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var OldFooter = _commonService.Delete(id);
                    _commonService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(OldFooter);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedFooters)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listFooter = new JavaScriptSerializer().Deserialize<List<int>>(checkedFooters);
                    foreach (var item in listFooter)
                    {
                        _commonService.Delete(Convert.ToString(item));
                    }
                    _commonService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, listFooter.Count);
                }

                return response;
            });
        }
    }
}
