using System;
using System.Collections.Generic;
using System.Linq;
using DiscountModule.Models;
using System.Text;
using DiscountModule.Config;
using System.IO;

namespace DiscountModule.BusinessLogic
{
    public class Validations
    {
        /// <summary>
        /// Parses the input data.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public InputDetails ParseInputData(string line)
        {
            var details = line.Split(' ');
            var date = string.Empty;
            var packageSize = string.Empty;
            var provider = string.Empty;
            try
            {
                date = details[0];
                packageSize = details[1];
                provider = details[2];
            }
            catch (Exception ex)
            {

            }
            return new InputDetails() { Date = date, PackageSize = packageSize, Provider = provider };

        }
        /// <summary>
        /// Validates the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="packageSize">Size of the package.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public bool Validate(string date, string packageSize, string provider)
        {
            Configuration config = new Configuration();
            var packageSizes = config.packageSizes;
            var providers = config.providers;
            DateTime temp;
            if (!DateTime.TryParse(date, out temp))
            {
                return false;
            }
            if (!packageSizes.Contains(packageSize))
            {
                return false;
            }
            if (!providers.Contains(provider))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether [is file exist].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is file exist]; otherwise, <c>false</c>.
        /// </returns>
        public bool isFileExist()
        {
            Configuration config = new Configuration();
            return File.Exists(config.textFile) ? true : false;
        }

    }
}
