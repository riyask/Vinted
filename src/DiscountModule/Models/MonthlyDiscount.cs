using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountModule.Models
{
    /// <summary>
    /// Model for monthly discount
    /// </summary>
    public class MonthlyDiscount
    {
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
    }
}
