using DiscountModule.Config;
using DiscountModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscountModule.BusinessLogic
{
    public class Rules
    {
        /// <summary>
        /// Inputs the data formatting.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public InputDetails InputDataFormatting(string line)
        {
            Validations validation = new Validations();
            Configuration config = new Configuration();
            //Parsing input data line
            var parsedInput = validation.ParseInputData(line);
            //Validating input data line
            var isValid = validation.Validate(parsedInput.Date, parsedInput.PackageSize, parsedInput.Provider);
            parsedInput.IsValid = isValid;
            if (isValid)
            {
                parsedInput.MonthYear = Convert.ToDateTime(parsedInput.Date).ToString(config.calendarMonth);
            }
            else
            {
                parsedInput.MonthYear = null;
            }
            return parsedInput;
        }

        /// <summary>
        /// Applies the rules.
        /// </summary>
        /// <param name="details">The details.</param>
        public List<OutputDetails> ApplyRules(List<InputDetails> details)
        {
            var output = new List<OutputDetails>();
            var monthlyDiscount = new List<MonthlyDiscount>();
            //Grouping based on month, provider and package size to calculate sequence
            var group = details.GroupBy(d => new { d.MonthYear, d.Provider, d.PackageSize });
            foreach (var i in group)
            {
                var c = 0;
                foreach (var j in i)
                {
                    j.PurchaseSequence = c + 1;
                    c = c + 1;
                }
            }
            foreach (var item in details)
            {
                var request = new OutputDetails() { PurchaseSequence = item.PurchaseSequence, Date = item.Date, IsValid = item.IsValid, PackageSize = item.PackageSize, Provider = item.Provider };
                //Applying All rules to calculate discount.
                var outPutLine = AllRules(request, monthlyDiscount);
                output.Add(outPutLine);
            }
            return output;
        }

        /// <summary>
        /// Ruleses the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="monthlyDiscount">The monthly discount.</param>
        public OutputDetails AllRules(OutputDetails output, List<MonthlyDiscount> monthlyDiscount)
        {
            Configuration config = new Configuration();
            output.Month = output.IsValid == true ? Convert.ToDateTime(output.Date).ToString(config.calendarMonth) : null;
            PricingPlan plan = new PricingPlan();
            var actualamount = plan.Pricing().Where(d => d.PackageSize == output.PackageSize && d.Provider == output.Provider).Select(f => f.Price).FirstOrDefault();
            
            if (output.PackageSize == PackageEnum.S.ToString())
            {
                //Rule 1: All S shipments should always match the lowest S package price among the providers.
                var lowestAmount = plan.Pricing().Where(d => d.PackageSize == output.PackageSize).Select(f => f.Price).Min();
                var discount = actualamount - lowestAmount;
                output.Price = lowestAmount;
                output.Discount = discount;

                //Rule 3: Accumulated discounts cannot exceed 10 € in a calendar month. If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
                CalculateMonthlyDiscount(monthlyDiscount, output);
            }
            else if (output.PackageSize == PackageEnum.L.ToString())
            {
                //Rule 2: Third L shipment via LP should be free, but only once a calendar month.
                if (output.PurchaseSequence == config.purchaseSequence && output.Provider == ProvidersEnum.LP.ToString())
                {
                    output.Price = 0;
                    output.Discount = plan.Pricing().Where(d => d.PackageSize == output.PackageSize && d.Provider == output.Provider).Select(f => f.Price).FirstOrDefault();
                }
                else
                {
                    output.Price = actualamount;
                    output.Discount = 0;
                }
                CalculateMonthlyDiscount(monthlyDiscount, output);
            }
            else if (output.PackageSize == PackageEnum.M.ToString())
            {
                output.Price = actualamount;
                output.Discount = 0;
                CalculateMonthlyDiscount(monthlyDiscount, output);
            }
            return output;
        }

        /// <summary>
        /// Rule 3: Accumulated discounts cannot exceed a limit in a calendar month. If there are not enough funds to fully cover a discount this calendar month, it should be covered partially..
        /// </summary>
        /// <param name="monthlyDiscount">The monthly discount.</param>
        /// <param name="output">The output.</param>
        private static void CalculateMonthlyDiscount(List<MonthlyDiscount> monthlyDiscount, OutputDetails output)
        {
            Configuration config = new Configuration();
            var Limit = config.discountLimit;
            if (monthlyDiscount.Any(f => f.Month == output.Month))
            {
                var previousTotalDiscount = monthlyDiscount.Where(f => f.Month == output.Month).FirstOrDefault().TotalDiscount;
                var currentDiscount = previousTotalDiscount + output.Discount;
                //if discount exceed than limit, If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
                if (currentDiscount > Limit)
                {
                    PricingPlan plan = new PricingPlan();
                    var actualamount = plan.Pricing().Where(d => d.PackageSize == output.PackageSize && d.Provider == output.Provider).Select(f => f.Price).FirstOrDefault();
                    output.Discount = Limit - previousTotalDiscount;
                    output.Price = actualamount - output.Discount;
                }
                else
                {
                    //if month and year already in this list, then it will update
                    monthlyDiscount.Where(s => s.Month == output.Month).Select(S => { S.TotalDiscount = currentDiscount; return S; }).ToList();
                }
            }
            else
            {
                //if there is no any month and year in this list, then it will add to list
                monthlyDiscount.Add(new MonthlyDiscount() { Month = output.Month, TotalDiscount = output.Discount });
            }
        }
    }
}
