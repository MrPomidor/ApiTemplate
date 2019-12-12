using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using App.Repositories;
using App.Customers.Repositories;
using Microsoft.EntityFrameworkCore;
using App.Customers.Database;
using App.Models;

namespace App.Customers
{
    public class Module:IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            // example of manually registered components
            container.Register(Component.For<ICustomersManager>().ImplementedBy<CustomerManager>().LifestyleTransient());
           
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<CustomerDbContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<CustomerDbContext>();
                // for test purpose we are using InMemory database
                builder.UseInMemoryDatabase("CustomerDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<CustomerDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            // DbContext object is Disposable, so it is needed to use "using" constraction
            using (var context = container.Resolve<CustomerDbContext>())
            {
                // add values to the context (without saving)
                context.Customers.AddRange(new[]
                {
                    new Customer { Id = 1, Name = "some1" },
                    new Customer { Id = 2, Name = "some2" },
                    new Customer { Id = 3, Name = "some3" },
                    new Customer { Id = 4, Name = "some4" },
                    new Customer { Id = 5, Name = "some5" },
                    new Customer { Id = 6, Name = "some6" },
                });

                // save changes in the context
                context.SaveChanges();
            }
        }
    }
}
