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
    [RoutePrefix("api/contactdetail")]
    [Authorize]
    public class ContactDetailController : ApiControllerBase
    {
        private IContactDetailService _contactDetailService;

        public ContactDetailController(IErrorService errorService, IContactDetailService contactDetailService)
            : base(errorService)
        {
            this._contactDetailService = contactDetailService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _contactDetailService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _contactDetailService.GetById(id);

                var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _contactDetailService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(query);

                var paginationSet = new PaginationSet<ContactDetailViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, ContactDetailViewModel contactDetailVm)
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
                    var newContactDetail = new ContactDetail();
                    newContactDetail.UpdateContactDetail(contactDetailVm);

                    _contactDetailService.Add(newContactDetail);
                    _contactDetailService.Save();

                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(newContactDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ContactDetailViewModel contactDetailVm)
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
                    var dbContactDetail = _contactDetailService.GetById(contactDetailVm.ID);
                    dbContactDetail.UpdateContactDetail(contactDetailVm);
                    _contactDetailService.Update(dbContactDetail);
                    _contactDetailService.Save();

                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(dbContactDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var OldContactDetail = _contactDetailService.Delete(id);
                    _contactDetailService.Save();

                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(OldContactDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedContactDetail)
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
                    var listContactDetail = new JavaScriptSerializer().Deserialize<List<int>>(checkedContactDetail);
                    foreach (var item in listContactDetail)
                    {
                        _contactDetailService.Delete(item);
                    }
                    _contactDetailService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, listContactDetail.Count);
                }

                return response;
            });
        }
    }
}
