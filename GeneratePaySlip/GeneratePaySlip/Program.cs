// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using GeneratePaySlip.Models;
using GeneratePaySlip.Interfaces;
using GeneratePaySlip;
using Serilog;
using System;
using System.IO.Abstractions;

string originFilePath = @"data\input.csv";
string destinationFilePath = @"data\output.csv";

#region obselete
//string InputFilePath =  @"data\input.csv";
//string OutputFilePath = @"data\output.csv";

//IPaySlipGenerator generator = new MonthlyPaySlipGenerator();
//ITaxCalculator Calculator = new MonthlyTaxCalculator();
//IFileContext file = new FileContext(InputFilePath, OutputFilePath, new FileSystem(), Log.Logger);
//List<Employee> emp = file.ReadFile().ToList();

//foreach (Employee e in emp)
//{
//    generator.GeneratePayslips(e, Calculator, Log.Logger);
//    foreach(PaySlip p in e.Payslips)
//        Console.WriteLine(e.FirstName +" "+ e.LastName + "," + p.StartDate.ToString("dd MMMM") + "-" + p.EndDate.ToString("dd MMMM") + ","+p.GrossIncome+","+p.IncomeTax+","+p.NetIncome+","+p.Super);
//}
//Console.ReadLine();
#endregion

// create service collection
var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection, originFilePath, destinationFilePath);

// create service provider
var serviceProvider = serviceCollection.BuildServiceProvider();

// entry to run app
serviceProvider.GetService<App>().Run();


static void ConfigureServices(IServiceCollection services, string originFilePath, string destinationFilePath)
{

    // configure logging
    Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs-main.txt")
    .WriteTo.Console()
    .CreateLogger();

    // add logging
    services.AddSingleton<ILogger>(Log.Logger);

    // add context
    services.AddScoped<IFileContext>(_ => new FileContext(originFilePath, destinationFilePath, new FileSystem(), Log.Logger));

    // add domain services
    services.AddTransient<ITaxCalculator, MonthlyTaxCalculator>();
    services.AddTransient<IPaySlipGenerator, MonthlyPaySlipGenerator>();

    // add app
    services.AddTransient<App>();
}