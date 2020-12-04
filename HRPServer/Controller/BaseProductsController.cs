using HRPData.Context;
using HRPData.Entity;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRPServer.Controller
{
    [Authorize]
    public class BaseProductsController : ODataController
    {
        private readonly HRPModel _model;

        public BaseProductsController(HRPModel model)
        {
            _model = model;
        }

        [HttpGet]
        public IEnumerable<BaseProduct> Get()
        {
            if (_model.BaseProducts.Count() == 0)
            {
                _model.Add<BaseProduct>(new BaseProduct());
                _model.SaveChanges();
            }
            return _model.BaseProducts;
        }
    }
}
