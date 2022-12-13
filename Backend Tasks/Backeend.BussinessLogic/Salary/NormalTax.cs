using Backend.Models;
using System;

namespace Backend.BussinessLogic.Salary
{
    internal class NormalTax : BaseDiscount
    {
        public double GetImport(double grossSalary)
        {
            SalaryDiscount salaryDiscount = new SalaryDiscount
            {
                Rate = Properties.Settings.Default.Tax,
                MaxBaseCalc = 0,
                MinIncome = Properties.Settings.Default.MinimumTaxIncome
            };
            return base.GetImport(grossSalary, salaryDiscount);
        }
    }
}
