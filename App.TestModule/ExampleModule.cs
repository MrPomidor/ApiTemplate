using App.Configuration;
using App.Example.Database;
using App.Example.Services;
using App.Models.Example;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;

namespace App.Example
{
	/// <summary>
	/// Endpoint class for registering the module in the system. This class should be referenced in the main module directly
	/// </summary>
	public class ExampleModule : IModule
    {
        /// <summary>
        /// This method initialize additional module dependencies, if it is not possible to use utility interfaces
        /// </summary>
        /// <param name="container"></param>
        public void Initialize(IWindsorContainer container)
        {
            // example of manually registered components
            container.Register(Component.For<IAnotherService>().ImplementedBy<AnotherService>().LifestyleTransient());

            RegisterDbContext(container);
        }

        /// <summary>
        /// Performs registering dependencies for using EntityFramework DbContext
        /// For more info, please, visit next respurces:
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext
        /// </summary>
        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<ExampleDbContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<ExampleDbContext>();
                // for test purpose we are using InMemory database
                builder.UseInMemoryDatabase("ExampleDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<ExampleDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        /// <summary>
        /// Performs initial seed of data for DbContext
        /// </summary>
        private void InitializeDbContext(IWindsorContainer container)
        {
            // DbContext object is Disposable, so it is needed to use "using" constraction
            using(var context = container.Resolve<ExampleDbContext>())
            {
                // add values to the context (without saving)
                context.SimpleValues.AddRange(new []
                {
                    new SimpleValue { Key = "1", Value = "1 - first values" },
                    new SimpleValue { Key = "2", Value = "2 - second values" },
                    new SimpleValue { Key = "3", Value = "3 - third values" }
                });

                // save changes in the context
                context.SaveChanges();
            }
        }
    }
}