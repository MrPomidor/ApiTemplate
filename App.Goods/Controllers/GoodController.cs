using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;
using App.Goods.Filters;

namespace App.Goods.Controllers
{
    [TypeFilter(typeof(GoodExceptionFilter), Arguments = new object[] { nameof(GoodController) })]
    [Route("api/goods")]
    [ApiController]
    public class GoodController: ControllerBase
    {
        readonly ILogger<GoodController> _logger;
        readonly IGoodsManager _goodsManager;
        readonly IOrderManager _orderManager;

        public GoodController(ILogger<GoodController>  logger, IGoodsManager goodsManager, IOrderManager orderManager)
        {
            _logger = logger;
            _goodsManager = goodsManager;
            _orderManager = orderManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Good>> Get()
        {
            _logger.LogInformation("Call Get goods method");
             var serviceCallResult = _goodsManager.GetGoods().ToList();
             return serviceCallResult;   
        }

        [HttpGet("{id}")]
        public Good GetById(int id)
        {
            _logger.LogInformation("Call Get by id good  method");

            {
                var serviceCallResult = _goodsManager.GetGood(id);
                return serviceCallResult;
            }
        }

        [HttpPost]
        [Route("createOrder")]
        public void AddOrder(Order order)
        {
            _logger.LogInformation("Call Add order method");
            _orderManager.AddOrder(order);
        }
    }
}
