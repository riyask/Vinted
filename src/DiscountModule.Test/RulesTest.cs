using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DiscountModule.BusinessLogic;
using DiscountModule.Models;
using System.Linq;

namespace DiscountModule.Test
{
    public class RulesTest
    {
        [Theory]
        [InlineData("2015-02-01 S MR")]
        [InlineData("2015-02-02 L MR")]
        [InlineData("2015-02-03 M MR")]
        [InlineData("2015-02-01 S LP")]
        [InlineData("2015-02-02 L LP")]
        [InlineData("2015-02-03 M LP")]
        public void InputDataFormattingTest(string line)
        {
            Rules rules = new Rules();
            var formattedInput = rules.InputDataFormatting(line);
            Assert.Equal(formattedInput.Date, line.Split(' ')[0]);
            Assert.Equal(formattedInput.PackageSize, line.Split(' ')[1]);
            Assert.Equal(formattedInput.Provider, line.Split(' ')[2]);
            Assert.NotNull(formattedInput.IsValid.ToString());
            Assert.Equal(true || false, formattedInput.IsValid);
            Assert.NotNull(formattedInput.MonthYear);
            Assert.NotNull(formattedInput.PurchaseSequence.ToString());
        }

        [Fact]
        public void ApplyRulesTest()
        {
            Rules rules = new Rules();
            List<InputDetails> inputDetails = new List<InputDetails>();
            inputDetails.Add(new InputDetails() { Date = "2015-02-01", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-02", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-03", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-05", PackageSize = "S", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-06", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-06", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-07", PackageSize = "L", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-08", PackageSize = "M", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-09", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-10", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-10", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-10", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-11", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-12", PackageSize = "M", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-13", PackageSize = "M", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-15", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-17", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-17", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-24", PackageSize = "L", Provider = "LP", IsValid = true, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-02-29", PackageSize = "CUSPS", Provider = " ", IsValid = false, PurchaseSequence = 0, });
            inputDetails.Add(new InputDetails() { Date = "2015-03-01", PackageSize = "S", Provider = "MR", IsValid = true, PurchaseSequence = 0, });
            var output = rules.ApplyRules(inputDetails);

            List<OutputDetails> outputDetailsSampleData = new List<OutputDetails>();

            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-01", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-02", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-03", PackageSize = "L", Provider = "LP", IsValid = true, Price = 6.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-05", PackageSize = "S", Provider = "LP", IsValid = true, Price = 1.50m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-06", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-06", PackageSize = "L", Provider = "LP", IsValid = true, Price = 6.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-07", PackageSize = "L", Provider = "MR", IsValid = true, Price = 4.00m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-08", PackageSize = "M", Provider = "MR", IsValid = true, Price = 3.00m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-09", PackageSize = "L", Provider = "LP", IsValid = true, Price = 0.00m, Discount = 6.90m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-10", PackageSize = "L", Provider = "LP", IsValid = true, Price = 6.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-10", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-10", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-11", PackageSize = "L", Provider = "LP", IsValid = true, Price = 6.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-12", PackageSize = "M", Provider = "MR", IsValid = true, Price = 3.00m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-13", PackageSize = "M", Provider = "LP", IsValid = true, Price = 4.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-15", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-17", PackageSize = "L", Provider = "LP", IsValid = true, Price = 6.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-17", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.90m, Discount = 0.10m });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-24", PackageSize = "L", Provider = "LP", IsValid = true, Price = 6.90m, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-02-29", PackageSize = "CUSPS", Provider = " ", IsValid = false, Price = 0, Discount = 0 });
            outputDetailsSampleData.Add(new OutputDetails() { Date = "2015-03-01", PackageSize = "S", Provider = "MR", IsValid = true, Price = 1.50m, Discount = 0.50m });

            int index = 0;
            foreach (var line in output)
            {
                var s = outputDetailsSampleData.ElementAt(index).Price;
                if (line.IsValid == outputDetailsSampleData.ElementAt(index).IsValid == true)
                {
                    Assert.Equal(line.Price, outputDetailsSampleData.ElementAt(index).Price);
                    Assert.Equal(line.Discount, outputDetailsSampleData.ElementAt(index).Discount);
                }
                else
                {
                    Assert.False(line.IsValid, "Ignored Input data");
                }
                index++;
            }
        }
    }
}