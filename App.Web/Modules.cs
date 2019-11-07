using App.Configuration;

namespace App.Web
{
    /// <summary>
    /// IMPORTANT ! In order to use classes and endpoints, defined in your own module, it should be referenced here as it shown
    /// </summary> 
    [ModuleUsing(typeof(Example.ExampleModule))]  // < ---- Example of module registration
	  [ModuleUsing(typeof(Accounts.AccountModule))]
    [ModuleUsing(typeof(Customers.Module))]
    [ModuleUsing(typeof(Goods.Module))]
    [ModuleUsing(typeof(Payments.PaymentsModule))]
    public class Modules
    {
    }
}
