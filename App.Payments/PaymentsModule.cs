using App.Configuration;
using App.Models;
using App.Payments.Database;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Payments
{
    public class PaymentsModule : IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<PaymentsDbContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<PaymentsDbContext>();
                builder.UseInMemoryDatabase("PaymentsDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<PaymentsDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            using (var context = container.Resolve<PaymentsDbContext>())
            { 
                context.payment.AddRange(new[]
                {
                    new Payment(1,"InProcesing"),
                    new Payment(2,"Success"),
                    new Payment(3,"Success")
                });
                context.SaveChanges();
            }
        }
    }
}
