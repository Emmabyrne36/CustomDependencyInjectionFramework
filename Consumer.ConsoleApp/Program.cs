using Consumer.ConsoleApp;
using Consumer.ConsoleApp.Interfaces;
using Vax;

var services = new ServiceCollection();

//services.AddSingleton<IConsoleWriter, ConsoleWriter>();
//services.AddSingleton<IIdGenerator, IdGenerator>();

// services.AddTransient<ConsoleWriter>();

//services.AddSingleton(new IdGenerator());

services.AddSingleton(provider => new IdGenerator());

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<IdGenerator>();
var service2 = serviceProvider.GetService<IdGenerator>();

//service?.WriteLine("Hello from my custom DI");

Console.WriteLine(service1.Id);
Console.WriteLine(service2.Id);