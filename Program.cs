using DI_Balta.Services;
using Microsoft.Extensions.DependencyInjection;

using(ServiceProvider container = RegisterServices())
{
    var primService = new PrimaryService();
    var secService = new SecondaryService(primService);
    var secService2 = new SecondaryService(primService);
    var terService = new TertiaryService(primService, secService, secService2);

    Console.WriteLine("*****     Primary Service ID: ADD SINGLETON     *****");
    Console.WriteLine(primService.Id + " - Primary Service ID - First instance called in Primary services");
    Console.WriteLine(secService.PrimaryServiceId + " - Primary Service ID - Second instance called in secondary services");
    Console.WriteLine(terService.PrimaryServiceId + " - Primary Service ID - Third instance called in tertiary services");

    Console.WriteLine("\n*****     Secondary Service ID: ADD SCOPED      *****");
    Console.WriteLine(secService.Id + " - Secondary Service ID - First instance called in secondary services");
    Console.WriteLine(terService.SecondaryServiceId + " - Secondary Service ID - Second instance called in tertiary services");
    Console.WriteLine(terService.SecondaryServiceNewInstanceID + " - Secondary Service ID - New instance inside tertiary service");

    Console.WriteLine("\n*****     Tertiary Service ID: ADD TRANSIENT    *****");
    Console.WriteLine(terService.Id + " - Tertiary Service ID - allways a new instance");
}

static ServiceProvider RegisterServices()
{
    var services = new ServiceCollection();
    services.AddSingleton<PrimaryService>();
    services.AddScoped<SecondaryService>();
    services.AddTransient<TertiaryService>();
    return services.BuildServiceProvider();
}