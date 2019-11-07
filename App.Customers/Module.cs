using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using App.Repositories;
using App.Customers.Repositories;


namespace App.Customers
{
    public class Module:IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            // example of manually registered components
            container.Register(Component.For<ICustomersManager>().ImplementedBy<CustomerManager>().LifestyleTransient());
            container.Register(Component.For<ICustomersRepository>().
                ImplementedBy<CustomersRepository>().LifestyleTransient());
        }
    }
}
