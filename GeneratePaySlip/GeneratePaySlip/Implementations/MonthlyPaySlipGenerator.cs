using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratePaySlip.Exceptions;
using GeneratePaySlip.Interfaces;
using GeneratePaySlip.Models;
using Serilog;

namespace GeneratePaySlip
{
    public class MonthlyPaySlipGenerator:IPaySlipGenerator
    {
        public MonthlyPaySlipGenerator() { }

        public void GeneratePayslips(Employee emp, ITaxCalculator taxCalculator, ILogger logger)
        {
            foreach (PaySlip p in emp.Payslips)
            {
                if (!p.Generated)
                {
                    try
                    {
                        GeneratePayslip(taxCalculator, p, emp.AnnualIncome, emp.SuperRate);
                    }
                    catch (NegativeNumberException ex)
                    {
                        logger.Error(string.Format("Negative Number error while generating payslip for {0} {1} : {2}", emp.FirstName, emp.LastName, ex.Message));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(string.Format("Error generating payslip for {0} {1} : {2}", emp.FirstName, emp.LastName, ex.Message));
                    }
                }
            }
        }

        public void GeneratePayslip(ITaxCalculator calculator, PaySlip payslip, uint annualIncome, double superRate)
        {
            try
            {
                payslip.GrossIncome = calculator.GrossIncome(annualIncome);
                payslip.IncomeTax = calculator.IncomeTax(annualIncome);
                payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                payslip.Super = calculator.Super(annualIncome, superRate);
                payslip.Generated = true;
            }
            catch (NegativeNumberException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
