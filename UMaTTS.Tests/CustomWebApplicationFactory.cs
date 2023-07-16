using Microsoft.AspNetCore.Mvc.Testing;

namespace UMaTLMS.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> 
    where TStartup : class
{
    
}