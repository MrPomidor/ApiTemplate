﻿using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using App.Repositories;
using App.Goods.Repositories;

namespace App.Goods
{
    public class Class1
    {
        public void Initialize(IWindsorContainer container)
        {
            // example of manually registered components
            container.Register(Component.For<IGoodsManager>().ImplementedBy<GoodsManager>().LifestyleTransient());
            container.Register(Component.For<IGodsRepository>().
                ImplementedBy<GoodsRepository>().LifestyleTransient());
        }
    }
}
