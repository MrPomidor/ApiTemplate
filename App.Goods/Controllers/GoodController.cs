using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;

namespace App.Goods.Controllers
{
    [Route("api/goods")]
    [ApiController]
    public class GoodController: ControllerBase
    {
        readonly IGoodsManager _goodsManager;
        readonly IOrderManager _orderManager;

        public GoodController(IGoodsManager goodsManager, IOrderManager orderManager)
        { 
            _goodsManager = goodsManager;
            _orderManager = orderManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Good>> Get()
        {
             var serviceCallResult = _goodsManager.GetGoods().ToList();
             return serviceCallResult;   
        }

        [HttpGet("{id}")]
        public ActionResult<Good> GetById(int id)
        {
            if (_goodsManager.GetGoods() == null)
                return NotFound();
            else
            {
                var serviceCallResult = _goodsManager.GetGood(id);
                return serviceCallResult;
            }
        }

        [HttpPost]
        [Route("createOrder")]
        public ActionResult AddOrder(Order order)
        {
            _orderManager.AddOrder(order);
            return Ok();
        }
    }
}
