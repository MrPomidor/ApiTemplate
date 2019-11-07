using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using App.Repositories;
using App.Goods.Repositories;

namespace App.Goods
{
    public class Module
    {
        public void Initialize(IWindsorContainer container)
        {
            // example of manually registered components
            container.Register(Component.For<IGoodsManager>().ImplementedBy<GoodsManager>().LifestyleTransient());
            container.Register(Component.For<IGodsRepository>().
                ImplementedBy<GoodsRepository>().LifestyleTransient());
            container.Register(Component.For<IOrderManager>().ImplementedBy<OrderManager>().LifestyleTransient());
            container.Register(Component.For<IOrderRepository>().
                ImplementedBy<OrderRepository>().LifestyleTransient());
        }
    }
}
