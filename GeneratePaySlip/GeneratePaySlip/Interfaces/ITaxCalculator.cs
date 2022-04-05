using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePaySlip.Interfaces
{
    public interface ITaxCalculator
    {
        uint GrossIncome(uint annualIncome);
        uint IncomeTax(uint annualIncome);
        uint NetIncome(uint annualIncome);
        uint Super(uint annualIncome, double superRate);
        TaxBand GetTaxBand(uint annualIncome);
    }
}
