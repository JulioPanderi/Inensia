using Backend.BussinessLogic.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BussinessLogic
{
    public class Salaries : ISalaries
    {
        public double CalculateNet(double grossSalary)
        {
            //Taxes
            NormalTax normalTax = new NormalTax();
            double taxes = normalTax.GetImport(grossSalary);

            //Social Contribution
            SocialContributionTax socialContributionTax = new SocialContributionTax();
            double socialContribution = socialContributionTax.GetImport(grossSalary);
            
            return grossSalary - taxes - socialContribution;
        }
    }
}