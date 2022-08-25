using DI_Balta.Services;
using Microsoft.Extensions.DependencyInjection;

using(ServiceProvider container = RegisterServices())
{

    var primService = new PrimaryService();
    var secService = new SecondaryService(primService);
    var secService2 = new SecondaryService(primService);
    var terService = new TertiaryService(primService, secService, secService2);

    Console.WriteLine("*****     Primary Service ID: ADD SINGLETON     *****");
    Console.WriteLine(primService.Id + " - Primary Service ID - First instance");
    Console.WriteLine(secService.PrimaryServiceId + " - Primary Service ID - Second instance");
    Console.WriteLine(terService.PrimaryServiceId + " - Primary Service ID - Third instance");

    Console.WriteLine("\n*****     Secondary Service ID: ADD SCOPED      *****");
    Console.WriteLine(secService.Id + " - Secondary Service ID - Second instance");
    Console.WriteLine(terService.SecondaryServiceId + " - Secondary Service ID - Thrid instance");
    Console.WriteLine(terService.SecondaryServiceNewInstanceID + " - Secondary Service ID - New instance inside third instance");

    Console.WriteLine("\n*****     Tertiary Service ID: ADD TRANSIENT    *****");
    Console.WriteLine(terService.Id + " - Tertiary Service ID");

    /*
    var primController = container.GetRequiredService<PrimaryService>();
    string primResult = primController.Id.ToString();
    Console.WriteLine("Primary Service ID: " + primResult);
    Console.WriteLine("----------------------------------------------------------");
    
    var secController = container.GetRequiredService<SecondaryService>();
    string secPrimResult = secController.PrimaryServiceId.ToString();
    string secResult = secController.Id.ToString();
    Console.WriteLine("Primary Service ID:" + secPrimResult);
    Console.WriteLine("Secondary Service ID:  " + secResult);
    Console.WriteLine("----------------------------------------------------------");

    var terController = container.GetRequiredService<TertiaryService>();

    string terPrimResult = terController.PrimaryServiceId.ToString();
    string terSecResult = terController.SecondaryServiceId.ToString();
    string terSecResult2 = terController.SecondaryServiceNewInstanceID.ToString();
    string terResult = terController.Id.ToString();
    Console.WriteLine("Primary Service ID: " + terPrimResult);
    Console.WriteLine("Secondary Service ID: " + terSecResult);
    Console.WriteLine("Secondary Service ID New Instance: " + terSecResult2);
    Console.WriteLine("Tertiary Service ID: " + terResult);
    Console.WriteLine("----------------------------------------------------------");*/
}

static ServiceProvider RegisterServices()
{
    var services = new ServiceCollection();
    services.AddSingleton<PrimaryService>();
    services.AddScoped<SecondaryService>();
    services.AddTransient<TertiaryService>();
    return services.BuildServiceProvider();
}