using DiscountModule.BusinessLogic;
using DiscountModule.Models;
using DiscountModule.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscountModule
{
    /// <summary>
    /// Program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var inputDetails = new List<InputDetails>();
            Validations validation = new Validations();
            Configuration config = new Configuration();
            //check file is exist or not
            if (validation.isFileExist())
            {
                //Read all line from input text file
                string[] lines = File.ReadAllLines(config.textFile);
                Rules rules = new Rules();
                foreach (string line in lines)
                {
                    var parsedInput = rules.InputDataFormatting(line);
                    inputDetails.Add(parsedInput);
                }
                //Applying Rules
                var outputDetails= rules.ApplyRules(inputDetails);
                foreach (var output in outputDetails)
                {
                    if (output.IsValid)
                    {
                        Console.WriteLine(output.Date + ' ' + output.PackageSize + ' ' + output.Provider + ' ' + String.Format("{0:F2}", output.Price) + ' ' + (output.Discount != 0 ? (String.Format("{0:F2}", output.Discount)).ToString() : "-"));
                    }
                    else
                    {
                        Console.WriteLine(output.Date + ' ' + output.PackageSize + " Ignored");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
