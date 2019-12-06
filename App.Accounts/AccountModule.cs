using App.Accounts.Database;
using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;

namespace App.Accounts
{
	public class AccountModule : IModule
	{
		public void Initialize(IWindsorContainer container)
		{
			RegisterDbContext(container);
		}
		private void RegisterDbContext(IWindsorContainer container)
		{
			container.Register(Component.For<DbContextOptions<AccountDbContext>>().UsingFactoryMethod(() =>
			{
				var builder = new DbContextOptionsBuilder<AccountDbContext>();
				builder.UseInMemoryDatabase("AccountDb");
				return builder.Options;
			}).LifestyleTransient());

			container.Register(Component.For<AccountDbContext>().LifestyleTransient());

			InitializeDbContext(container);
		}
		private void InitializeDbContext(IWindsorContainer container)
		{
			// DbContext object is Disposable, so it is needed to use "using" constraction
			using (var context = container.Resolve<AccountDbContext>())
			{
				// add values to the context (without saving)
				context.Accounts.AddRange(new[]
				{
					new Account(1,false),
					new Account(2,true),
					new Account (3, false)
				});

				// save changes in the context
				context.SaveChanges();
			}
		}
	}
}
