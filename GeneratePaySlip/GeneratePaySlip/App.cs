using GeneratePaySlip.Interfaces;
using GeneratePaySlip.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePaySlip
{
    public class App
    {

        private readonly ILogger Logger;

        private ITaxCalculator _taxCalculator;
        private IPaySlipGenerator _paySlipGenerator;
        private IFileContext _fileContext;

        public App(ITaxCalculator taxCalculator, IPaySlipGenerator paySlipGenerator,IFileContext fileContext, ILogger logger)
        {
            _taxCalculator = taxCalculator;
            _paySlipGenerator = paySlipGenerator;
            _fileContext = fileContext;
            this.Logger = logger;
        }

        public void Run()
        {
            //IPaySlipGenerator generator = new MonthlyPaySlipGenerator();
            //ITaxCalculator Calculator = new MonthlyTaxCalculator();
            //IFileContext file = new FileContext(InputFilePath, OutputFilePath, new FileSystem(), Log.Logger);
            List<Employee> emp = _fileContext.ReadFile().ToList();

            foreach (Employee e in emp)
            {
                _paySlipGenerator.GeneratePayslips(e, _taxCalculator, Log.Logger);
                foreach (PaySlip p in e.Payslips)
                    Console.WriteLine(e.FirstName + " " + e.LastName + "," + p.StartDate.ToString("dd MMMM") + "-" + p.EndDate.ToString("dd MMMM") + "," + p.GrossIncome + "," + p.IncomeTax + "," + p.NetIncome + "," + p.Super);
            }
            Console.ReadLine();
        }
    }
}
