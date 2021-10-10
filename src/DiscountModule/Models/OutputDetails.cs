using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountModule.Models
{
    /// <summary>
    /// Model for output details
    /// </summary>
    public class OutputDetails
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
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string Month { get; set; }
        /// <summary>
        /// Gets or sets the total discount.
        /// </summary>
        /// <value>
        /// The total discount.
        /// </value>
        public decimal TotalDiscount { get; set; }
        /// <summary>
        /// Gets or sets the purchase sequence.
        /// </summary>
        /// <value>
        /// The purchase sequence.
        /// </value>
        public int PurchaseSequence { get; set; }
    }
}
