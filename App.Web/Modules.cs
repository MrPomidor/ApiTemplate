﻿using App.Configuration;

namespace App.Web
{
    /// <summary>
    /// IMPORTANT ! In order to use classes and endpoints, defined in your own module, it should be referenced here as it shown
    /// </summary>
    [ModuleUsing(typeof(Example.ExampleModule))]
	[ModuleUsing(typeof(Accounts.AccountModule))]
    public class Modules
    {
    }
}
