using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BussinessLogic.Salary
{
    internal abstract class BaseDiscount
    {
        internal Double GetImport(double grossSalary, SalaryDiscount salaryDiscount)
    {
            double taxes = 0;

            if (grossSalary > salaryDiscount.MinIncome)
            {
                if (grossSalary > salaryDiscount.MaxBaseCalc && salaryDiscount.MaxBaseCalc > 0)
                {
                    taxes = salaryDiscount.MaxBaseCalc * salaryDiscount.Rate;
                }
                else
                {
                    taxes = grossSalary * salaryDiscount.Rate;
                }
            }
            return taxes;
        }
    }
}
