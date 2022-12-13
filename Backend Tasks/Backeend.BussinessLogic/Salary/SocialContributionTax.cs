using Backend.Models;
using System;

namespace Backend.BussinessLogic.Salary
{
    internal class SocialContributionTax : BaseDiscount
    {
        public double GetImport(double grossSalary)
        {
            SalaryDiscount salaryDiscount = new SalaryDiscount
            {
                Rate = Properties.Settings.Default.SocialContribution,
                MaxBaseCalc = Properties.Settings.Default.MaxSocialContribution,
                MinIncome = 0
            };
            return base.GetImport(grossSalary, salaryDiscount);
        }
    }
}
