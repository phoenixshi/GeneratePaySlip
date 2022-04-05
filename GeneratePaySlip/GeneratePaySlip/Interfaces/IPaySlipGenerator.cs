using GeneratePaySlip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace GeneratePaySlip.Interfaces
{
    public interface IPaySlipGenerator
    {
        void GeneratePayslips(Employee emp, ITaxCalculator taxCalculator, ILogger logger);
        void GeneratePayslip(ITaxCalculator calculator, PaySlip payslip, uint annualIncome, double superRate);
    }
}
