using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using App.Repositories;
using App.Goods.Repositories;
using App.Goods.Database;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Goods
{
    public class Module:IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<GoodsDbContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<GoodsDbContext>();
                builder.UseInMemoryDatabase("GoodsDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<GoodsDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            using (var context = container.Resolve<GoodsDbContext>())
            {
                context.Goods.AddRange(new[]
                {
                    new Good { Id = 1,Name = "Kaptsi", Price = 20 },
                    new Good { Id = 2,Name = "Sapogi", Price = 25},
                    new Good { Id = 3,Name = "Kufajka", Price = 2}
                });

                context.SaveChanges();
            }
        }
    }
}
