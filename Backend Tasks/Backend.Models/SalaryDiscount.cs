using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class SalaryDiscount
    {
        /// <summary>
        /// Tax rate
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Minimal income to calc tax
        /// </summary>
        public double MinIncome { get; set; }

        /// <summary>
        /// Maximum income to take as base
        /// </summary>
        public double MaxBaseCalc { get; set; }
    }
}
