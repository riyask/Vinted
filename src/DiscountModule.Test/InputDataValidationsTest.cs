using DiscountModule.BusinessLogic;
using System;
using Xunit;

namespace DiscountModule.Test
{
    public class InputDataValidationsTest
    {
        [Fact]
        public void CheckInputFileExist()
        {
            Validations validation = new Validations();
            var valid = validation.isFileExist();
            Assert.True(valid, "Input file is exist");
        }

        [Theory]
        [InlineData("2015-02-01 S MR")]
        [InlineData("2015-02-02 L MR")]
        [InlineData("2015-02-03 M MR")]
        [InlineData("2015-02-01 S LP")]
        [InlineData("2015-02-02 L LP")]
        [InlineData("2015-02-03 M LP")]
        public void ParseCorrectInputDataRow(string line)
        {
            Validations validation = new Validations();
            var parsedInputData = validation.ParseInputData(line);
            Assert.NotNull(parsedInputData);
        }

        [Theory]
        [InlineData("2015-02-0001 S MR")]
        [InlineData("2015-02-02 L MAAR")]
        [InlineData("2015-02-03 M MRCC")]
        [InlineData("2015-02-01 SSS LP")]
        [InlineData("2015-02-02 K LP")]
        [InlineData("2015-02-03")]
        public void ParseIgnoredInputDataRow(string line)
        {
            Validations validation = new Validations();
            var parsedInputData = validation.ParseInputData(line);
            Assert.NotNull(parsedInputData);
        }

        [Theory]
        [InlineData("2015-02-01", "S", "MR")]
        [InlineData("2015-02-02", "L", "MR")]
        [InlineData("2015-02-03", "M", "MR")]
        [InlineData("2015-02-01", "S", "LP")]
        [InlineData("2015-02-02", "L", "LP")]
        [InlineData("2015-02-03", "M", "LP")]
        public void CorrectDataValidations(string date, string packageSize, string provider)
        {
            Validations validation = new Validations();
            var valid= validation.Validate(date, packageSize, provider);
            Assert.True(valid, "it is valid data input");
        }

        [Theory]
        [InlineData("2015-02-29", "CUSPS","")]
        public void IgnoredDataValidations(string date, string packageSize, string provider)
        {
            Validations validation = new Validations();
            var valid = validation.Validate(date, packageSize, provider);
            Assert.False(valid, "it is ignored data input");
        }
    }
}
