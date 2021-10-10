using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountModule.Models
{
    /// <summary>
    /// Model for input details
    /// </summary>
    public class InputDetails
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date { get; set; }
        /// <summary>
        /// Gets or sets the size of the package.
        /// </summary>
        /// <value>
        /// The size of the package.
        /// </value>
        public string PackageSize { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public string Provider { get; set; }
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }
        /// <summary>
        /// Gets or sets the purchase sequence.
        /// </summary>
        /// <value>
        /// The purchase sequence.
        /// </value>
        public int PurchaseSequence { get; set; }
        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        public string MonthYear { get; set; }
    }
}
